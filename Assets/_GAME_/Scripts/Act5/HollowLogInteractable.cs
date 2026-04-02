using System.Collections;
using UnityEngine;

public class HollowLogInteractable : MonoBehaviour, IInteractable
{   
    [Header("Dependencies")]
    [SerializeField] private ForestNight2Manager forestNight2Manager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform playerCutscenePos;

    [Header("Audio")]
    [SerializeField] private AudioClip tobaccoOfferSound;
    [SerializeField] private AudioClip melodicWhistleSound;

    private bool isRunning;

    private const string HollowLogNarration =
        "Lucas para diante de um tronco grosso. À primeira vista, ele parece igual aos outros.\n\n" +
        "Mas, ao se aproximar, percebe uma abertura escura na madeira: o interior é oco.";

    private static readonly string[] OfferLines =
    {
        "<color=#531182>Lucas:</color> O bilhete do vovô...",
        "E o jornal também falou disso.",
        "Se tem alguém guardando essa trilha... acho que é aqui que eu devo deixar o fumo."
    };

    private static readonly string[] ResultLines =
    {
        "<color=#531182>Lucas:</color> Obrigado... eu acho.",
        "O vovô estava certo.",
        "Respeitar o que é deles é o único jeito de sobreviver aqui."
    };

    public void Interact()
    {
        if (isRunning)
            return;

        if (ProgressionManager.Instance.act5ForestLoopBroken)
            return;

        if (!forestNight2Manager.CanUseHollowLog)
            return;

        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {

        isRunning = true;

        GameStateManager.SetState(GameState.Cutscene);

        yield return playerController.MoveTo(playerCutscenePos.position,2f);

        playerController.ForceFaceUp();

        yield return NarrationUI.Instance.ShowTextRoutine(HollowLogNarration);

        GameStateManager.SetState(GameState.Thought);
        yield return ThoughtUI.Instance.PlaySequence(OfferLines);

        AudioManager.Instance.PlaySFX(tobaccoOfferSound);
        
        yield return new WaitForSecondsRealtime(0.8f);

        AudioManager.Instance.PlaySFX(melodicWhistleSound);

        yield return new WaitForSecondsRealtime(1f);

        GameStateManager.SetState(GameState.Thought);
        yield return ThoughtUI.Instance.PlaySequence(ResultLines);

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;

        forestNight2Manager.BreakForestLoop();
    }
}