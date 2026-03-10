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

    public void CompleteTask(string id)
    {
        completedTasks.Add(id);
    }

    public bool IsCompleted(string id)
    {
        return completedTasks.Contains(id);
    }
}