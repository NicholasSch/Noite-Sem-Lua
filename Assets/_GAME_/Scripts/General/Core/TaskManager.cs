using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    private readonly HashSet<string> completedTasks = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (ProgressionManager.Instance == null)
            return;

        completedTasks.Clear();

        foreach (string task in ProgressionManager.Instance.completedTaskIDs)
        {
            completedTasks.Add(task);
        }
    }

    public void CompleteTask(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return;

        if (completedTasks.Add(id))
        {
            ProgressionManager.Instance.CompleteTask(id);
        }
    }

    public bool IsCompleted(string id)
    {
        return completedTasks.Contains(id);
    }
}