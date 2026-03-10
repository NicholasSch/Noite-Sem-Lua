using UnityEngine;
using System.Collections.Generic;

public class JournalSystem : MonoBehaviour
{
    public static JournalSystem Instance;

    class Task
    {
        public string id;
        public string title;
        public string description;

        public Task(string id, string title, string description)
        {
            this.id = id;
            this.title = title;
            this.description = description;
        }
    }

    List<Task> tasks = new List<Task>();

    string leftPageText;

    void Awake()
    {
        Instance = this;
        SetupDay1();
    }

    void SetupDay1()
    {
        leftPageText =
        "Caderno de Dante\n\n" +
        "O Engenho não gosta de estranhos.\n" +
        "Caminhe pelos limites e mostre à terra\n" +
        "que o sangue de Dante ainda corre aqui.";

        tasks.Clear();

        tasks.Add(new Task("Barn_Tools","O Reconhecimento do Chão: (Interaja com o celeiro)", 
        "O Engenho não gosta de estranhos. Caminhe pelos limites, toque as ferramentas e mostre à terra que o sangue de Dante ainda corre aqui."));

        tasks.Add(new Task("Mill_Gears",
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
            bool completed = TaskManager.Instance.IsCompleted(task.id);
            string checkbox = completed ? "[X] " : "[ ] ";

            text += checkbox + task.title + "\n";
            text += "   " + task.description + "\n\n";
        }

        return text;
    }
}