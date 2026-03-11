using System.Collections.Generic;
using UnityEngine;

public class JournalSystem : MonoBehaviour
{
    public static JournalSystem Instance { get; private set; }

    private class Task
    {
        public string Id { get; }
        public string Title { get; }
        public string Description { get; }

        public Task(string id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }

    private readonly List<Task> tasks = new();

    private string leftPageText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SetupDay1();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetupDay1()
    {
        leftPageText =
            "Caderno de Dante\n\n" +
            "O Engenho não gosta de estranhos.\n" +
            "Caminhe pelos limites e mostre à terra\n" +
            "que o sangue de Dante ainda corre aqui.";

        tasks.Clear();

        tasks.Add(new Task(
            "Barn_Tools",
            "O Reconhecimento do Chão: (Interaja com o celeiro)",
            "O Engenho não gosta de estranhos. Caminhe pelos limites, toque as ferramentas e mostre à terra que o sangue de Dante ainda corre aqui."
        ));

        tasks.Add(new Task(
            "Mill_Gears",
            "Coração de Pedra: (Interaja com o moinho)",
            "O moinho parou quando eu me cansei. Verifique se as engrenagens ainda lembram como girar. Elas guardam o que o vento traz."
        ));
    }

    public string GetLeftPage()
    {
        return leftPageText;
    }

    public string GetRightPage()
    {
        string text = "Tarefas\n\n";

        foreach (Task task in tasks)
        {
            bool completed = TaskManager.Instance.IsCompleted(task.Id);
            string checkbox = completed ? "[X] " : "[ ] ";

            text += checkbox + task.Title + "\n";
            text += "   " + task.Description + "\n\n";
        }

        return text;
    }
}