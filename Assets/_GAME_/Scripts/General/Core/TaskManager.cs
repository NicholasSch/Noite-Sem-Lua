using UnityEngine;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    HashSet<string> completedTasks = new HashSet<string>();

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {   
        foreach (var task in ProgressionManager.Instance.completedTaskIDs) 
        {

            completedTasks.Add(task);
            
        }
    }

    public void CompleteTask(string id)
    {

        completedTasks.Add(id);
        ProgressionManager.Instance.completedTaskIDs.Add(id);
    }

    public bool IsCompleted(string id)
    {
        return completedTasks.Contains(id);
    }
}