using System.Collections;
using UnityEngine;

public class MarketDay2Manager : MonoBehaviour
{
    public enum VendorType
    {
        Feirante1,
        Feirante2,
        Feirante3,
        Feirante4
    }

    [Header("Audio")]
    [SerializeField] private AudioClip marketAmbience;
    [SerializeField] private AudioClip marketMusic;
    [SerializeField] private AudioClip radioSong;

    [Header("Dependencies")]
    [SerializeField] private PlayerController player;

    [Header("NPC Controllers")]
    [SerializeField] private NPCController feirante1Controller;
    [SerializeField] private NPCController feirante2Controller;
    [SerializeField] private NPCController feirante3Controller;
    [SerializeField] private NPCController feirante4Controller;

    [Header("Objects")]
    [SerializeField] private GameObject DantesRadio;

    private bool isInteractionRunning;

    private const string Feirante1NpcID = "feirante1";
    private const string Feirante2NpcID = "feirante2";
    private const string Feirante3NpcID = "feirante3";
    private const string Feirante4NpcID = "feirante4";

    private static readonly string[] Feirante1FirstLines =
    {
        "Feirante: Óleo de lamparina... sementes de hortaliça... é isso que ocê vai levar, rapaz?",
        "<color=#531182>Lucas:</color> É. Só isso.",
        "Feirante: Pois tá certo.",
    };

        private static readonly string[] Feirante1SecondLines =
    {
        "Feirante: ...",
        "Feirante: Escuta um tiquinho só.",
        "Esse rádio velho vive chiando e puxando essa música.",
        "Feirante: Era dum homem lá do Engenho.",
        "Feirante: Vendeu faz muitos anos, quando a mulher dele adoeceu.",
        "Feirante: Disse que num aguentava o silêncio que ficava depois que a música acabava.",
        "<color=#531182>Lucas:</color> Ele se desfez de tudo que lembrava ela... pra tentar parar de sofrer.",
        "Mas o Engenho não deixa ninguém esquecer.",
        "Feirante: Se ocê quiser levar esse rádio junto, eu faço preço de ocasião.",
        "<color=#531182>Lucas:</color> ...Levo."
    };

    private static readonly string[] Feirante1AfterLines =
    {
        "<color=#531182>Lucas:</color> Óleo, sementes... e agora esse rádio.",
        "Parece que até o que saiu do Engenho dá um jeito de voltar."
    };

    private static readonly string[] Feirante1RepeatLines =
    {
        "Feirante: Ocê já levou o que precisava, rapaz.",
        "Feirante: Melhor num ficar remoendo música triste por muito tempo, não."
    };

    private static readonly string[] Feirante2FirstLines =
    {
        "Feirante: Dona Curió?",
        "Feirante: Ô menino... tem nome que a gente num chama assim, no claro do dia, no meio da feira.",
        "Feirante: Se essa mulher falou contigo, tenha cuidado.",
        "Aqui na região, dívida antiga nunca se paga com dinheiro.",
        "<color=#531182>Lucas:</color> Ninguém conhece ela direito, mas todo mundo teme o que ela representa.",
        "Por que o vovô deixou ela entrar?"
    };

    private static readonly string[] Feirante2RepeatLines =
    {
        "Feirante: Certos nome é melhor deixar quieto, meu filho."
    };

    private static readonly string[] Feirante3FirstLines =
    {
        "Feirante: Dona Curió? Eu não conheci.",
        "Feirante: Mas o povo daqui muda de assunto ligeiro quando esse nome aparece.",
        "Feirante: E povo da roça num treme à toa, viu?",
        "<color=#531182>Lucas:</color> Então ninguém fala... mas ninguém acha normal também.",
        "Tem alguma coisa errada nisso tudo."
    };

    private static readonly string[] Feirante3RepeatLines =
    {
        "Feirante: Melhor ocê comprar o que precisa e seguir seu rumo, moço."
    };

    private static readonly string[] Feirante4FirstLines =
    {
        "Feirante: Dona Curió?",
        "Feirante: Rapaz, nesse sol quente assim ocê tá é vendo assombração.",
        "Feirante: Num existe ninguém com esse nome por essas banda, não.",
        "Feirante: E se existisse, uma hora ou outra eu já tinha escutado.",
        "<color=#531182>Lucas:</color> Eles falam como se fosse bobagem...",
        "mas sempre com medo de dizer demais."
    };

    private static readonly string[] Feirante4RepeatLines =
    {
        "Feirante: Vai com calma, rapaz. Feira demais também embaralha a cabeça."
    };

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(marketAmbience);
        AudioManager.Instance.PlayMusic(marketMusic);
        DantesRadio.SetActive(!ProgressionManager.Instance.act4RadioBought);
    }

    public void InteractWithVendor(VendorType vendorType)
    {
        if (isInteractionRunning)
            return;

        StartCoroutine(InteractionRoutine(vendorType));
    }

    private IEnumerator InteractionRoutine(VendorType vendorType)
    {
        isInteractionRunning = true;
        GameStateManager.SetState(GameState.Cutscene);

        NPCController controller = GetController(vendorType);
        controller.LookAtTarget(player.transform);
        player.ForceFaceUp();

        switch (vendorType)
        {
            case VendorType.Feirante1:
                yield return HandleFeirante1();
                break;

            case VendorType.Feirante2:
                yield return HandleFeirante2();
                break;

            case VendorType.Feirante3:
                yield return HandleFeirante3();
                break;

            case VendorType.Feirante4:
                yield return HandleFeirante4();
                break;
        }

        GameStateManager.SetState(GameState.Gameplay);
        isInteractionRunning = false;
    }

    private IEnumerator HandleFeirante1()
    {
        bool hasTalkedBefore = ProgressionManager.Instance.HasTalkedToNpc(Feirante1NpcID);

        if (!hasTalkedBefore)
        {   
            StartCoroutine(AudioManager.Instance.FadeOutMusicRoutine(2f));

            yield return ThoughtUI.Instance.PlaySequence(Feirante1FirstLines);

            StartCoroutine(AudioManager.Instance.FadeInMusicRoutine(radioSong,2f));

            yield return ThoughtUI.Instance.PlaySequence(Feirante1SecondLines);

            yield return StartCoroutine(AudioManager.Instance.FadeOutMusicRoutine(2f));

            StartCoroutine(AudioManager.Instance.FadeInMusicRoutine(marketMusic,2f));

            ProgressionManager.Instance.RegisterNpcTalk(Feirante1NpcID);

            if (!TaskManager.Instance.IsCompleted("Market_Supplies"))
                TaskManager.Instance.CompleteTask("Market_Supplies");

            ProgressionManager.Instance.act4RadioBought = true;
            DantesRadio.SetActive(false);
            ProgressionManager.Instance.SaveProgress();

            yield return ThoughtUI.Instance.PlaySequence(Feirante1AfterLines);
            yield break;
        }

        yield return ThoughtUI.Instance.PlaySequence(Feirante1RepeatLines);
    }

    private IEnumerator HandleFeirante2()
    {
        bool hasTalkedBefore = ProgressionManager.Instance.HasTalkedToNpc(Feirante2NpcID);

        if (!hasTalkedBefore)
        {
            yield return ThoughtUI.Instance.PlaySequence(Feirante2FirstLines);
            ProgressionManager.Instance.RegisterNpcTalk(Feirante2NpcID);
            yield break;
        }

        yield return ThoughtUI.Instance.PlaySequence(Feirante2RepeatLines);
    }

    private IEnumerator HandleFeirante3()
    {
        bool hasTalkedBefore = ProgressionManager.Instance.HasTalkedToNpc(Feirante3NpcID);

        if (!hasTalkedBefore)
        {   
            yield return ThoughtUI.Instance.PlaySequence(Feirante3FirstLines);
            ProgressionManager.Instance.RegisterNpcTalk(Feirante3NpcID);
            yield break;
        }

        yield return ThoughtUI.Instance.PlaySequence(Feirante3RepeatLines);
    }

    private IEnumerator HandleFeirante4()
    {
        bool hasTalkedBefore = ProgressionManager.Instance.HasTalkedToNpc(Feirante4NpcID);

        if (!hasTalkedBefore)
        {
            yield return ThoughtUI.Instance.PlaySequence(Feirante4FirstLines);
            ProgressionManager.Instance.RegisterNpcTalk(Feirante4NpcID);
            yield break;
        }

        yield return ThoughtUI.Instance.PlaySequence(Feirante4RepeatLines);
    }

    private NPCController GetController(VendorType vendorType)
    {
        switch (vendorType)
        {
            case VendorType.Feirante1:
                return feirante1Controller;

            case VendorType.Feirante2:
                return feirante2Controller;

            case VendorType.Feirante3:
                return feirante3Controller;

            default:
                return feirante4Controller;
        }
    }
}