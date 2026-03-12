using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance { get; private set; }

    public enum DayPeriod
    {
        Day,
        Night
    }

    [System.Serializable]
    private class SaveData
    {
        public int currentDay;
        public DayPeriod currentPeriod;
        public bool farmIntroPlayed;
        public bool LetterOpened;
        public bool porchScenePlayed;
        public bool firstNightSleepDone;
        public bool firstNightWakeScenePlayed;
        public bool firstNightTitlePlayed;
        public string pendingSpawnPointID;
        public string pendingSceneName;
        public List<string> completedTaskIDs = new();
        public List<string> talkedNpcIDs = new();
    }

    public int currentDay = 0;
    public DayPeriod currentPeriod = DayPeriod.Day;

    public bool farmIntroPlayed;
    public bool LetterOpened;
    public bool porchScenePlayed;
    public bool firstNightSleepDone;
    public bool firstNightWakeScenePlayed;
    public bool firstNightTitlePlayed;

    public string pendingSpawnPointID;
    public string pendingSceneName;

    public List<string> completedTaskIDs = new();
    public List<string> talkedNpcIDs = new();

    private string SavePath => Path.Combine(Application.persistentDataPath, "progression.json");

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompleteTask(string taskID)
    {
        if (string.IsNullOrWhiteSpace(taskID))
            return;

        if (!completedTaskIDs.Contains(taskID))
        {
            completedTaskIDs.Add(taskID);
            SaveProgress();
        }
    }

    public bool IsTaskCompleted(string taskID)
    {
        return completedTaskIDs.Contains(taskID);
    }

    public void RegisterNpcTalk(string npcID)
    {
        if (string.IsNullOrWhiteSpace(npcID))
            return;

        if (!talkedNpcIDs.Contains(npcID))
        {
            talkedNpcIDs.Add(npcID);
            SaveProgress();
        }
    }

    public bool HasTalkedToNpc(string npcID)
    {
        if (string.IsNullOrWhiteSpace(npcID))
            return false;

        return talkedNpcIDs.Contains(npcID);
    }

    public void SetDay(int day)
    {
        currentDay = Mathf.Max(0, day);
        SaveProgress();
    }

    public void SetPeriod(DayPeriod period)
    {
        currentPeriod = period;
        SaveProgress();
    }

    public void AdvanceToNextDay()
    {
        currentDay++;
        currentPeriod = DayPeriod.Day;
        SaveProgress();
    }

    public void SetPendingSpawn(string sceneName, string spawnPointID)
    {
        pendingSceneName = sceneName;
        pendingSpawnPointID = spawnPointID;
        SaveProgress();
    }

    public void ClearPendingSpawn()
    {
        pendingSceneName = null;
        pendingSpawnPointID = null;
        SaveProgress();
    }

    public void SaveProgress()
    {
        SaveData data = new SaveData
        {
            currentDay = currentDay,
            currentPeriod = currentPeriod,
            farmIntroPlayed = farmIntroPlayed,
            LetterOpened = LetterOpened,
            porchScenePlayed = porchScenePlayed,
            firstNightSleepDone = firstNightSleepDone,
            firstNightWakeScenePlayed = firstNightWakeScenePlayed,
            firstNightTitlePlayed = firstNightTitlePlayed,
            pendingSpawnPointID = pendingSpawnPointID,
            pendingSceneName = pendingSceneName,
            completedTaskIDs = new List<string>(completedTaskIDs),
            talkedNpcIDs = new List<string>(talkedNpcIDs)
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public void LoadProgress()
    {
        if (!File.Exists(SavePath))
            return;

        string json = File.ReadAllText(SavePath);

        if (string.IsNullOrWhiteSpace(json))
            return;

        SaveData data = JsonUtility.FromJson<SaveData>(json);

        if (data == null)
            return;

        currentDay = Mathf.Max(1, data.currentDay);
        currentPeriod = data.currentPeriod;
        farmIntroPlayed = data.farmIntroPlayed;
        LetterOpened = data.LetterOpened;
        porchScenePlayed = data.porchScenePlayed;
        firstNightSleepDone = data.firstNightSleepDone;
        firstNightWakeScenePlayed = data.firstNightWakeScenePlayed;
        firstNightTitlePlayed = data.firstNightTitlePlayed;
        pendingSpawnPointID = data.pendingSpawnPointID;
        pendingSceneName = data.pendingSceneName;
        completedTaskIDs = data.completedTaskIDs ?? new List<string>();
        talkedNpcIDs = data.talkedNpcIDs ?? new List<string>();
    }

    public void ResetProgress()
    {
        currentDay = 1;
        currentPeriod = DayPeriod.Day;
        farmIntroPlayed = false;
        LetterOpened = false;
        porchScenePlayed = false;
        firstNightSleepDone = false;
        firstNightWakeScenePlayed = false;
        firstNightTitlePlayed = false;
        pendingSpawnPointID = null;
        pendingSceneName = null;
        completedTaskIDs.Clear();
        talkedNpcIDs.Clear();

        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }
    }

    private void OnApplicationQuit()
    {
        SaveProgress();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveProgress();
        }
    }
}