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

            case ProgressionManager.JournalPhase.Day5Act8:
                SetupDay5Act8();
                break;
            case ProgressionManager.JournalPhase.Day5Act9:
                SetupDay5Act9();
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
            "Na vila há o que a terra não oferece óleo para as luzes e sementes para o amanhã. Não se detenha mais do que o necessário."
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
            "A terra começou a recusar meus passos antes mesmo de eu compreender o preço do que pedi. " +
            "Onde antes havia fruto, agora há cinza; onde havia canto, restou apenas um silêncio que pesa mais do que pedra.\n\n" +
            "Tentei esconder sob o moinho aquilo que meus olhos já não suportavam lembrar, " +
            "mas o chão deste Engenho não foi feito para guardar mentiras por muito tempo.\n\n" +
            "Há marcas que o vento não apagou e há coisas que nem a própria terra aceitou engolir. " +
            "Se o rastro seco ainda resistiu, é porque certas memórias se recusam a morrer.\n\n" +
            "E mesmo na ruína... ainda há quem vigie, em silêncio, o que um dia pertenceu à luz dela.";

        tasks.Add(new Task(
            "Act7_FirstDig",
            "O Primeiro Buraco (Cave na base do moinho)",
            "O rastro sem cor leva ao moinho como se obedecesse a uma mágoa antiga. Onde a terra foi remexida e o peso do passado se acumulou junto às engrenagens paradas, cave. Há coisas que foram ocultadas, mas não entregues ao esquecimento."
        ));

        tasks.Add(new Task(
            "Act7_SecondDig",
            "O Segundo Buraco (Cave na base da árvore cortada)",
            "Nem tudo o que resta neste Engenho se move para ferir. Sob a sombra do moinho, haverá um gesto, um chamado, um último vestígio de vontade. Quando ele surgir, siga-o. Há memórias que só retornam quando a terra é aberta pela segunda vez."
        ));
    }

    private void SetupDay5Act8()
    {
        leftPageText =
            "Caderno de Dante\n\n" +
            "Nem toda raiz deste chão apodreceu com a dor. Houve algo que a terra escondeu da bruxa, " +
            "como se ainda guardasse, em seu fundo mais limpo, um resto de misericórdia para o último dos meus.\n\n" +
            "A árvore branca bebeu o que o Engenho não conseguiu perder. " +
            "Ela cresceu em silêncio sobre aquilo que era puro e tomou para si a parte da terra que a névoa não conseguiu corromper.\n\n" +
            "Mas pureza sozinha não basta contra o hálito do norte. " +
            "O que o vento trouxe, o que o fogo espera e o que o solo preservou devem se encontrar no mesmo fôlego.\n\n" +
            "Só então a fumaça deixará de ser fumaça, e o medo deixará de encontrar caminho pelos pulmões.";

        tasks.Add(new Task(
            "Act8_TreeHeart",
            "O Coração da Árvore (Pegue o fruto da árvore branca)",
            "A árvore branca reteve em seu corpo aquilo que o solo ainda tinha de limpo. Tome o fruto que ela oferecer. Nele repousa uma parte do Engenho que a dor não conseguiu apodrecer."
        ));

        tasks.Add(new Task(
            "Act8_FireRitual",
            "O Ritual na Fogueira (Prepare a proteção)",
            "Na fogueira central, reúna o que veio pelo vento, o que foi poupado pela terra e o que o tempo não conseguiu apagar. Quando essas partes se unirem na brasa, a fumaça deixará de cegar e passará a guardar o fôlego de quem segue para o norte."
        ));
    }

private void SetupDay5Act9()
{
    leftPageText =
        "Caderno de Dante\n\n" +
        "O Engenho já não pede, nem implora como antes. Tudo o que podia ser levado foi levado, e tudo o que podia apodrecer já cedeu à terra. O que restou não cresce nem morre; apenas permanece, como se o próprio tempo tivesse desistido de seguir adiante.\n\n" +
        "Há muito a terra deixou de responder aos vivos. Agora, ela escuta apenas aquilo que insiste em ficar, aquilo que não aceita partir mesmo quando tudo ao redor já se foi.\n\n" +
        "O que caminha por estes campos não é vento, nem memória. É fome. E a fome aprendeu o seu nome.\n\n" +
        "Tentei enterrar o que não devia existir. Tentei queimar o que não podia continuar. Tentei esquecer, como se o esquecimento fosse capaz de apagar o que esta terra insiste em guardar. Mas há coisas que não aceitam fim; elas esperam, crescem em silêncio e sempre encontram o caminho de volta.\n\n" +
        "Se você chegou até aqui, não foi porque venceu o Engenho. Foi porque ele permitiu. E agora, pela primeira vez desde que tudo começou, ele não pretende mais esperar.";

    tasks.Add(new Task(
        "Act9_DefeatCurio",
        "O Último Encontro (Enfrente Curio)",
        "Ele não se esconde mais nas margens nem sussurra entre as árvores. Curio caminha livre pelo Engenho, e onde seus passos tocam, a terra se curva como se reconhecesse o próprio dono.\n\n" +
        "Não há mais rituais, nem proteção, nem promessas capazes de adiar o inevitável. Resta apenas o encontro entre você e aquilo que nunca deixou este lugar.\n\n" +
        "Termine o que foi começado — ou torne-se parte do que permanece."
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