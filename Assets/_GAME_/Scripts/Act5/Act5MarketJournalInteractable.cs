using System.Collections;
using UnityEngine;
public class Act5MarketJournalInteractable : MonoBehaviour, IInteractable
{   
    [Header("Dependencies")]
    [SerializeField] private MarketNight2Manager marketNight2Manager;

    [Header("Audio")]
    [SerializeField] private AudioClip paperSound;
    [SerializeField] private AudioClip softImpactSound;

    private bool isRunning;

    private static readonly string[] JournalLines =
    {
        "<color=#531182>Lucas:</color> Aqui... caiu aqui mesmo."
    };

    private const string PhotoNarration =
        "Ao erguer o caderno, algo escorrega de dentro das páginas e cai no chão.\n\n" +
        "É uma fotografia antiga, amarelada pelo tempo. Nela, Dante já idoso segura um bebê no colo, sentado no banco do pomar.\n\n" +
        "O mesmo banco. O mesmo lugar.";

    private const string BackNarration =
        "No verso, a caligrafia é trêmula:\n\n" +
        "\"Para que ele nunca herde minhas sombras,\n" +
        "mas sempre encontre o caminho de volta para o sol.\"";

    private static readonly string[] LucasLines =
    {
        "<color=#531182>Lucas:</color> Ele sabia...",
        "Ele sabia que eu viria.",
        "Ele não me deixou instruções pra me assustar...",
        "Ele me deixou um mapa...",
        "Pra eu não me perder onde ele se perdeu."
    };

    public void Interact()
    {
        if (isRunning || ProgressionManager.Instance.act5JournalRecovered)
            return;

        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        isRunning = true;

        GameStateManager.SetState(GameState.Cutscene);

        AudioManager.Instance.PlaySFX(paperSound);
        yield return ThoughtUI.Instance.PlaySequence(JournalLines);

        AudioManager.Instance.PlaySFX(softImpactSound);
        yield return NarrationUI.Instance.ShowTextRoutine(PhotoNarration);

        yield return NarrationUI.Instance.ShowTextRoutine(BackNarration);

        GameStateManager.SetState(GameState.Thought);
        yield return ThoughtUI.Instance.PlaySequence(LucasLines);

        marketNight2Manager.MarkJournalInteracted();

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;
    }
}