using UnityEngine;
using System.Collections.Generic;

public class JournalSystem : MonoBehaviour
{
    public static JournalSystem Instance;

    class Task
    {
        public string title;
        public string description;
        public bool completed;

        public Task(string title, string description)
        {
            this.title = title;
            this.description = description;
            completed = false;
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

        tasks.Add(new Task(
            "Reconhecimento do Chão",
            "O Engenho não gosta de estranhos. Caminhe pelos limites, toque as ferramentas e mostre à terra que o sangue de Dante ainda corre aqui."
        ));

        tasks.Add(new Task(
            "Coração de Pedra",
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
            string checkbox = task.completed ? "☑ " : "☐ ";

            text += checkbox + task.title + "\n";
            text += "   " + task.description + "\n\n";
        }

        return text;
    }
}