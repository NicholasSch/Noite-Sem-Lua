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
            SetupCurrentDay();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetupCurrentDay()
    {
        tasks.Clear();

        switch (ProgressionManager.Instance.currentDay)
        {
            case 1:
                SetupDay1();
                break;
            case 2:
                SetupDay2();
                break;
            default:
                SetupDay1();
                break;
        }
    }

    private void SetupDay1()
    {
        leftPageText =
            "Caderno de Dante\n\n" +
            "O Engenho não gosta de estranhos.\n" +
            "Caminhe pelos limites e mostre à terra\n" +
            "que o sangue de Dante ainda corre aqui.";

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

    private void SetupDay2()
    {
        leftPageText =
            "Caderno de Dante\n\n" +
            "Lia dizia que o Engenho só tinha vida por causa do pomar. Cada árvore aqui foi plantada com um desejo. " +
            "Se você encontrar o banco de madeira sob a sombra mais antiga, saberá que este lugar já foi preenchido por risos, " +
            "antes do silêncio se tornar o único dono da casa.";

        tasks.Add(new Task(
            "Orchard_Care",
            "O Cuidado com o Pomar",
            "Lia dizia que o Engenho só tinha vida pelas árvores. Cada uma foi um desejo. Tire o peso dos galhos secos para que o passado não sufoque o presente."
        ));

        tasks.Add(new Task(
            "Plant_Hope",
            "O Plantio da Esperança",
            "Plante o que dará sombra aos seus netos. Uma vida nova ajuda a terra a perdoar quem chega."
        ));

        tasks.Add(new Task(
            "Lake_Toll",
            "O Pedágio das Águas",
            "Não ignore o que vive no lago. Jogue uma moeda de prata. É o preço para que a água não te veja como um invasor."
        ));
    }

    public string GetLeftPage()
    {
        SetupCurrentDay();
        return leftPageText;
    }

    public string GetRightPage()
    {
        SetupCurrentDay();

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