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
        public bool ForceCompleted { get; } 
        public Task(string id, string title, string description, bool forceCompleted = false)
        {
            Id = id;
            Title = title;
            Description = description;
            ForceCompleted = forceCompleted;
        }
    }

    private readonly List<Task> tasks = new();
    private string leftPageText;

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
        SetupCurrentDay();
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
            case ProgressionManager.JournalPhase.Epilogue:
                SetupEpilogue();
                break;
            default:
                SetupDay1();
                break;
        }
    }

    private void SetupDay1()
    {
        leftPageText = "Caderno de Dante\n\nO Engenho não gosta de estranhos.\n\nCaminhe pelos limites e mostre à terra que o sangue de Dante ainda corre aqui.";
        tasks.Add(new Task("Barn_Tools", "O Reconhecimento do Chão", "Toque as ferramentas no celeiro. Mostre à terra que você pertence aqui."));
        tasks.Add(new Task("Mill_Gears", "Coração de Pedra", "Verifique as engrenagens do moinho. Elas guardam o que o vento traz."));
    }

    private void SetupDay2Act3()
    {
        leftPageText = "Caderno de Dante\n\nLia dizia que o Engenho só respirava por causa do pomar. Cada árvore nasceu de um desejo.\n\nSe encontrar o velho banco de madeira, talvez entenda que este lugar já pertenceu aos risos.";
        tasks.Add(new Task("Orchard_Care", "O Cuidado com o Pomar", "Colha o que ainda resiste nos arbustos para que o passado não sufoque o presente."));
        tasks.Add(new Task("Plant_Hope", "O Plantio da Esperança", "Plante a muda no canteiro. Uma vida nova ajuda a terra a perdoar."));
        tasks.Add(new Task("Lake_Toll", "O Pedágio das Águas", "Jogue a moeda no lago. É o preço para que a água não te veja como invasor."));
    }

    private void SetupDay2Act4()
    {
        leftPageText = "Caderno de Dante\n\nO que foi feito por amor resiste mais do que devia. Na vila, escute pouco.\n\nNeste lugar, até a ausência fala mais verdade do que gente viva.";
        tasks.Add(new Task("Trail_Marker", "O Trilho da Saudade", "Limpe o marco de pedra. A mata tenta apagar o que o coração insiste em lembrar."));
        tasks.Add(new Task("Market_Supplies", "O Abastecimento", "Busque óleo para as luzes e sementes para o amanhã. Não dê ouvidos ao que dizem nas bancas."));
    }

    private void SetupDay3Act6()
    {
        leftPageText = "Caderno de Dante\n\nO sangue da terra é a água. Cuide daquela que você plantou.\n\nSe o vento assobiar dentro de casa, não tente prendê-lo. Apenas guarde o que é frágil.";
        tasks.Add(new Task("Sentinel_Thirst", "A Sede da Sentinela", "Regue o pinheiro. Ele será seus olhos quando a névoa subir."));
        tasks.Add(new Task("House_Whistle", "A Casa que Assobia", "Prepare a proteção na fogueira central para afastar o redemoinho."));
    }

    private void SetupDay4Act7()
    {
        leftPageText = "Caderno de Dante\n\nOnde antes havia fruto, agora há cinza. O moinho não foi feito para guardar mentiras por muito tempo.\n\nSe o rastro seco resistiu, é porque certas memórias se recusam a morrer.";
        tasks.Add(new Task("Act7_FirstDig", "O Primeiro Buraco", "Cave na base do moinho. O cinza é a cor do que foi esquecido."));
        tasks.Add(new Task("Act7_SecondDig", "O Segundo Buraco", "Siga o rastro. Há memórias que só retornam quando a terra é aberta pela segunda vez."));
    }

    private void SetupDay5Act8()
    {
        leftPageText = "Caderno de Dante\n\nA árvore branca bebeu o que o Engenho não conseguiu perder.\n\nO fumo, a pena e o fruto. Respire a fumaça; ela será sua armadura contra o hálito da bruxa.";
        tasks.Add(new Task("Act8_TreeHeart", "O Coração da Árvore", "Pegue a pinha da árvore branca. Ela filtrou a dor deste solo."));
        tasks.Add(new Task("Act8_FireRitual", "O Ritual na Fogueira", "Queime a pena e o fruto. Prepare seus pulmões para a névoa do norte."));
    }

    private void SetupDay5Act9()
    {
        leftPageText = "Caderno de Dante\n\nO que restou não cresce nem morre; apenas permanece. Tentei esquecer, mas há coisas que não aceitam fim.\n\nElas esperam, crescem em silêncio e sempre encontram o caminho de volta.";
        tasks.Add(new Task("Act9_DefeatCurio", "O Acerto de Contas", "Enfrente a Cuca na Garganta da Rocha. Devolva o que o medo roubou."));
    }

    private void SetupEpilogue()
    {
        leftPageText = "Caderno de Lucas\n\nVim ao Engenho buscar as cinzas de um homem que eu mal conhecia, mas encontrei um lugar onde o tempo havia parado de respirar.\n\nO vovô não era feito de maldade; ele era feito de um amor tão pesado que se tornou pedra.\n\nHoje, o vento finalmente levou o que era do vento. O Engenho de Dante não é mais feito de sombras. A promessa foi cumprida.\n\nPode descansar, vovô. Eu cuidarei do resto.";
        tasks.Add(new Task("Final_Walk","O Novo Alvorecer","Caminhe até o pinheiro adulto. Veja o que o tempo e o cuidado foram capazes de reconstruir."));
        tasks.Add(new Task("Carry_Legacy","Carregar o Legado de Dante","O Engenho não é mais feito de pedras ou engrenagens. É feito de histórias. E hoje, uma nova história começa.",true ));
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
            bool completed = task.ForceCompleted || TaskManager.Instance.IsCompleted(task.Id);
            string checkbox = completed ? "[X] " : "[ ] ";

            text += checkbox + task.Title + "\n";
            text += "   " + task.Description + "\n\n";
        }

        return text;
    }
}