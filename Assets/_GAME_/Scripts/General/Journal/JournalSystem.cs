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

        switch (ProgressionManager.Instance.journalPhase)
        {
            case ProgressionManager.JournalPhase.Day1:
                SetupDay1();
                break;

            case ProgressionManager.JournalPhase.Day2Act3:
                SetupDay2Act3();
                break;

            case ProgressionManager.JournalPhase.Day2Act4:
                SetupDay2Act4();
                break;

            case ProgressionManager.JournalPhase.Day3Act6:
                SetupDay3Act6();
                break;
                
            case ProgressionManager.JournalPhase.Day4Act7:
                SetupDay4Act7();
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
            "O Engenho não gosta de estranhos.\n\n" +
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

    private void SetupDay2Act3()
    {
        leftPageText =
            "Caderno de Dante\n\n" +
            "Lia dizia que o Engenho só respirava por causa do pomar. Cada árvore nasceu de um desejo, de uma esperança confiada à terra.\n\n" +
            "Se encontrar o velho banco de madeira junto ao canteiro que já foi sombra e abrigo, talvez entenda que este lugar já pertenceu aos risos,\n\n" +
            "antes que o silêncio tomasse conta de tudo.";

        tasks.Add(new Task(
            "Orchard_Care",
            "O Cuidado com o Pomar (Interaja com os arbustos)",
            "Lia dizia que o Engenho só tinha vida por causa das árvores e dos frutos. Colha o que ainda resiste nos arbustos do pomar, para que o abandono não apague de vez o cuidado que existiu aqui."
        ));

        tasks.Add(new Task(
            "Plant_Hope",
            "O Plantio da Esperança (Plante a muda no canteiro vazio)",
            "Plante no canteiro aquilo que ainda pode crescer. Uma vida nova talvez ajude esta terra a lembrar que nem toda promessa termina em ruína."
        ));

        tasks.Add(new Task(
            "Lake_Toll",
            "O Pedágio das Águas (Interaja com o lago)",
            "Não ignore o que repousa nas águas. Ofereça a moeda de prata, como pede o caderno. Há lugares onde a terra acolhe, e outros onde é preciso pedir licença."
        ));
    }

    private void SetupDay2Act4()
    {
        leftPageText =
            "Caderno de Dante\n\n" +
            "Há caminhos que a mata tenta engolir, mas o coração insiste em lembrar. Mesmo coberto de lodo e silêncio, o que foi feito por amor resiste mais do que devia.\n\n" +
            "E há coisas que esta terra não dá, por mais que o Engenho peça em silêncio. Quando precisar buscar fora o que falta aqui dentro, vá sem se demorar.\n\n" +
            "Na vila, escute pouco. As vozes de fora confundem. Neste lugar, até a ausência fala mais verdade do que gente viva.";

        tasks.Add(new Task(
            "Trail_Marker",
            "O Trilho da Saudade (Limpe o marco no caminho da floresta)",
            "Há marcas que o tempo tenta cobrir, mas algumas permanecem, mesmo sob lodo e esquecimento."
        ));

        tasks.Add(new Task(
            "Market_Supplies",
            "O Abastecimento do Engenho (Fale com o feirante e consiga os mantimentos)",
            "Busque na vila o que a terra não oferece: óleo para as luzes e sementes para o amanhã. Não se detenha mais do que o necessário."
        ));
    }

    private void SetupDay3Act6()
    {
        leftPageText =
            "Caderno de Dante\n\n" +
            "O sangue da terra é a água, e a árvore que você plantou tem sede de séculos. " +
            "Cuide dela, pois ela será seus olhos quando a névoa subir.\n\n" +
            "E fique atento: nem todo vento sopra para limpar o céu; alguns ventos vêm em uma perna só " +
            "para bagunçar o que está no lugar.\n\n" +
            "Se o vento assobiar dentro de casa, não tente prendê-lo. Apenas guarde o que é frágil.";

        tasks.Add(new Task(
            "Sentinel_Thirst",
            "A Sede da Sentinela (Verifique a árvore que cresceu no canteiro)",
            "O que foi plantado ontem já reclama um tempo que não nos pertence. A muda estica-se para o céu como se buscasse algo que o Engenho esqueceu de dar. Ouça o que o tronco diz sob a casca e sacie a sede daquela que vigia o canteiro."
        ));

        tasks.Add(new Task(
            "House_Whistle",
            "A Casa que Assobia (Prepare a proteção na fogueira)",
            "Quando o assobio ecoar entre as frestas, o fogo será o único limite entre o que está dentro e o que quer entrar. Não deixe a chama morrer; prepare o cerco na fogueira antes que a escuridão tome o que é seu por direito."
        ));
    }

    private void SetupDay4Act7()
{
    leftPageText =
        "Caderno de Dante\n\n" +
        "A terra começou a recusar meus passos antes mesmo de eu entender o que havia feito. " +
        "Onde antes havia fruto, agora há cinza. Onde havia promessa, restou silêncio.\n\n" +
        "Tentei enganar o tempo, mas foi a própria terra que me julgou. " +
        "Ela não esquece o que é enterrado com pressa, nem aceita aquilo que não deveria ter voltado.\n\n" +
        "Se seguir o rastro que secou sob meus pés, encontrará aquilo que tentei esconder de mim mesmo. " +
        "E talvez compreenda por que o vento já não canta como antes.\n\n" +
        "Ainda assim... mesmo naquilo que restou de mim, algo insiste em guardar o que era dela.";

    tasks.Add(new Task(
        "Act7_FirstDig",
        "O Primeiro Buraco (Cave na base do moinho)",
        "Siga o rastro onde a relva perdeu a cor e deixe que o moinho conduza seus passos. Onde a terra já foi mexida, cave. O que foi enterrado ali não pertence ao esquecimento."
    ));

    tasks.Add(new Task(
        "Act7_SecondDig",
        "O Segundo Buraco (Cave na base da arvore cortada)",
        "Observe o que permanece sob a sombra do moinho e não recuse o sinal quando ele surgir. Há memórias que só retornam quando a terra é aberta pela segunda vez."
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