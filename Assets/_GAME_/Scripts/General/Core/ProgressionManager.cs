using UnityEngine;
using System.Collections.Generic;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance;

    //Global Data
    public int currentday;

    //Intercations Done
    public bool LetterOpened = false;
    public bool PorchScene = false;
    
    //"NPC States"
   public bool talkedToDonaCurio = false;

    //"Task States"
    public List<string> completedTaskIDs = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void CompleteTask(string taskID)
    {
        if (!completedTaskIDs.Contains(taskID))
            completedTaskIDs.Add(taskID);
    }
}