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

    public enum JournalPhase
    {
        Day1,
        Day2Act3,
        Day2Act4,
        Day3Act6,
        Day4Act7,
        Day5Act8, 
        Day5Act9
    }

    [System.Serializable]
    private class SaveData
    {
        public int currentDay;
        public DayPeriod currentPeriod;
        public JournalPhase journalPhase;
        public bool LetterOpened;
        public bool farmIntroPlayed;
        public bool act2CurioEncounterPlayed;
        public bool porchScenePlayed;
        public bool firstNightSleepDone;
        public bool firstNightWakeScenePlayed;
        public bool firstNightTitlePlayed;
        public bool act3MorningIntroPlayed;
        public bool act3BenchVisionSeen;
        public bool act3NewspaperFound;
        public bool act4Started;
        public bool act4RadioBought;
        public bool act4CurioEncounterPlayed;
        public bool act4HideGameUI;
        public bool act5NightIntroPlayed;
        public bool act5TobaccoFound;
        public bool act5JournalRecovered;
        public bool act5NewspaperFound;
        public bool act5FootstepsSeen;
        public bool act5ForestLoopBroken;
        public bool act5CaveBlockedSeen;
        public bool act6MorningIntroPlayed;
        public bool act6NewspaperFound;
        public bool act6NightStarted;
        public bool act6NightChaosPlayed;
        public bool act6RadioVisionSeen;
        public bool act6NoteFound;
        public bool act6CaveClueRevealed;
        public bool act7MorningIntroPlayed;
        public bool act7TrailObserved;
        public bool act7FirstDigInteracted;
        public bool act7NewspaperFound;
        public bool act7MillMessageFound;
        public bool act7SecondDigRevealed;
        public bool act7PocketWatchFound;
        public bool act8MorningIntroPlayed;
        public bool act8PineconeFound;
        public bool act8NewspaperFound;
        public bool act8RitualDone;
        public bool act8HairFound;
        public bool act9IntroPlayed;
        public string pendingSpawnPointID;
        public string pendingSceneName;
        public List<string> completedTaskIDs = new();
        public List<string> talkedNpcIDs = new();
    }

    public int currentDay = 0;
    public DayPeriod currentPeriod = DayPeriod.Day;
    public JournalPhase journalPhase = JournalPhase.Day1;

    public bool LetterOpened;
    public bool farmIntroPlayed;
    public bool act2CurioEncounterPlayed;
    public bool porchScenePlayed;
    public bool firstNightSleepDone;
    public bool firstNightWakeScenePlayed;
    public bool firstNightTitlePlayed;
    public bool act3MorningIntroPlayed;
    public bool act3BenchVisionSeen;
    public bool act3NewspaperFound;
    public bool act4Started;
    public bool act4RadioBought;
    public bool act4CurioEncounterPlayed;
    public bool act4HideGameUI;
    public bool act5NightIntroPlayed;
    public bool act5TobaccoFound;
    public bool act5JournalRecovered;
    public bool act5NewspaperFound;
    public bool act5FootstepsSeen;
    public bool act5ForestLoopBroken;
    public bool act5CaveBlockedSeen;
    public bool act6MorningIntroPlayed;
    public bool act6NewspaperFound;
    public bool act6NightStarted;
    public bool act6NightChaosPlayed;
    public bool act6RadioVisionSeen;
    public bool act6NoteFound;
    public bool act6CaveClueRevealed;
    public bool act7MorningIntroPlayed;
    public bool act7TrailObserved;
    public bool act7FirstDigInteracted;
    public bool act7NewspaperFound;
    public bool act7MillMessageFound;
    public bool act7SecondDigRevealed;
    public bool act7PocketWatchFound;
    public bool act8MorningIntroPlayed;
    public bool act8PineconeFound;
    public bool act8NewspaperFound;
    public bool act8RitualDone;
    public bool act8HairFound;
    public bool act9IntroPlayed;
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

    public void SetJournalPhase(JournalPhase phase)
    {
        journalPhase = phase;
        SaveProgress();
    }

    public void NextDay()
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
            journalPhase = journalPhase,
            LetterOpened = LetterOpened,
            farmIntroPlayed = farmIntroPlayed,
            act2CurioEncounterPlayed = act2CurioEncounterPlayed,
            porchScenePlayed = porchScenePlayed,
            firstNightSleepDone = firstNightSleepDone,
            firstNightWakeScenePlayed = firstNightWakeScenePlayed,
            firstNightTitlePlayed = firstNightTitlePlayed,
            act3MorningIntroPlayed = act3MorningIntroPlayed,
            act3BenchVisionSeen = act3BenchVisionSeen,
            act3NewspaperFound = act3NewspaperFound,
            act4Started = act4Started,
            act4RadioBought = act4RadioBought,
            act4CurioEncounterPlayed = act4CurioEncounterPlayed,
            act4HideGameUI = act4HideGameUI,
            act5NightIntroPlayed = act5NightIntroPlayed,
            act5TobaccoFound = act5TobaccoFound,
            act5JournalRecovered = act5JournalRecovered,
            act5NewspaperFound = act5NewspaperFound,
            act5FootstepsSeen = act5FootstepsSeen,
            act5ForestLoopBroken = act5ForestLoopBroken,
            act5CaveBlockedSeen = act5CaveBlockedSeen,
            act6MorningIntroPlayed = act6MorningIntroPlayed,
            act6NewspaperFound = act6NewspaperFound,
            act6NightStarted = act6NightStarted,
            act6NightChaosPlayed = act6NightChaosPlayed,
            act6RadioVisionSeen = act6RadioVisionSeen,
            act6NoteFound = act6NoteFound,
            act6CaveClueRevealed = act6CaveClueRevealed,
            act7MorningIntroPlayed = act7MorningIntroPlayed,
            act7TrailObserved = act7TrailObserved,
            act7FirstDigInteracted = act7FirstDigInteracted,
            act7NewspaperFound = act7NewspaperFound,
            act7MillMessageFound = act7MillMessageFound,
            act7SecondDigRevealed = act7SecondDigRevealed,
            act7PocketWatchFound = act7PocketWatchFound,
            act8MorningIntroPlayed = act8MorningIntroPlayed,
            act8PineconeFound = act8PineconeFound,
            act8NewspaperFound = act8NewspaperFound,
            act8RitualDone = act8RitualDone,
            act8HairFound = act8HairFound,
            act9IntroPlayed = act9IntroPlayed,
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

        currentDay = Mathf.Max(0, data.currentDay);
        currentPeriod = data.currentPeriod;
        journalPhase = data.journalPhase;
        LetterOpened = data.LetterOpened;
        farmIntroPlayed = data.farmIntroPlayed;
        act2CurioEncounterPlayed = data.act2CurioEncounterPlayed;
        porchScenePlayed = data.porchScenePlayed;
        firstNightSleepDone = data.firstNightSleepDone;
        firstNightWakeScenePlayed = data.firstNightWakeScenePlayed;
        firstNightTitlePlayed = data.firstNightTitlePlayed;
        act3MorningIntroPlayed = data.act3MorningIntroPlayed;
        act3BenchVisionSeen = data.act3BenchVisionSeen;
        act3NewspaperFound = data.act3NewspaperFound;
        act4Started = data.act4Started;
        act4RadioBought = data.act4RadioBought;
        act4CurioEncounterPlayed = data.act4CurioEncounterPlayed;
        act4HideGameUI = data.act4HideGameUI;
        act5NightIntroPlayed = data.act5NightIntroPlayed;
        act5TobaccoFound = data.act5TobaccoFound;
        act5JournalRecovered = data.act5JournalRecovered;
        act5NewspaperFound = data.act5NewspaperFound;
        act5FootstepsSeen = data.act5FootstepsSeen;
        act5ForestLoopBroken = data.act5ForestLoopBroken;
        act5CaveBlockedSeen = data.act5CaveBlockedSeen;
        act6MorningIntroPlayed = data.act6MorningIntroPlayed;
        act6NewspaperFound = data.act6NewspaperFound;
        act6NightStarted = data.act6NightStarted;
        act6NightChaosPlayed = data.act6NightChaosPlayed;
        act6RadioVisionSeen = data.act6RadioVisionSeen;
        act6NoteFound = data.act6NoteFound;
        act6CaveClueRevealed = data.act6CaveClueRevealed;
        act7MorningIntroPlayed = data.act7MorningIntroPlayed;
        act7TrailObserved = data.act7TrailObserved;
        act7FirstDigInteracted = data.act7FirstDigInteracted;
        act7NewspaperFound = data.act7NewspaperFound;
        act7MillMessageFound = data.act7MillMessageFound;
        act7SecondDigRevealed = data.act7SecondDigRevealed;
        act7PocketWatchFound = data.act7PocketWatchFound;
        act8MorningIntroPlayed = data.act8MorningIntroPlayed;
        act8PineconeFound = data.act8PineconeFound;
        act8NewspaperFound = data.act8NewspaperFound;
        act8RitualDone = data.act8RitualDone;
        act8HairFound = data.act8HairFound;
        act9IntroPlayed = data.act9IntroPlayed;
        pendingSpawnPointID = data.pendingSpawnPointID;
        pendingSceneName = data.pendingSceneName;
        completedTaskIDs = data.completedTaskIDs ?? new List<string>();
        talkedNpcIDs = data.talkedNpcIDs ?? new List<string>();
    }

    public void ResetProgress()
    {
        currentDay = 0;
        currentPeriod = DayPeriod.Day;
        journalPhase = JournalPhase.Day1;
        LetterOpened = false;
        farmIntroPlayed = false;
        act2CurioEncounterPlayed = false;
        porchScenePlayed = false;
        firstNightSleepDone = false;
        firstNightWakeScenePlayed = false;
        firstNightTitlePlayed = false;
        act3MorningIntroPlayed = false;
        act3BenchVisionSeen = false;
        act3NewspaperFound = false;
        act4Started = false;
        act4RadioBought = false;
        act4CurioEncounterPlayed = false;
        act4HideGameUI = false;
        act5NightIntroPlayed = false;
        act5TobaccoFound = false;
        act5JournalRecovered = false;
        act5NewspaperFound = false;
        act5FootstepsSeen = false;
        act5ForestLoopBroken = false;
        act5CaveBlockedSeen = false;
        act6MorningIntroPlayed = false;
        act6NewspaperFound = false;
        act6NightStarted = false;
        act6NightChaosPlayed = false;
        act6RadioVisionSeen = false;
        act6NoteFound = false;
        act6CaveClueRevealed = false;
        act7MorningIntroPlayed = false;
        act7TrailObserved = false;
        act7FirstDigInteracted = false;
        act7NewspaperFound = false;
        act7MillMessageFound = false;
        act7SecondDigRevealed = false;
        act7PocketWatchFound = false;
        act8MorningIntroPlayed = false;
        act8PineconeFound = false;
        act8NewspaperFound = false;
        act8RitualDone = false;
        act8HairFound = false;
        act9IntroPlayed = false;
        pendingSpawnPointID = null;
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
            SaveProgress();
    }
}