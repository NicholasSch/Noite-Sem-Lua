using System.Collections;
using UnityEngine;

public class CurupiraFootstepsInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private ForestNight2Manager forestNight2Manager;
    [SerializeField] private CurupiraEncounterController encounterController;

    private bool isRunning;

    private static readonly string[] Lines =
    {
        "<color=#531182>Lucas:</color> Que estranho...",
        "Quem conseguiria caminhar assim?",
        "As pegadas indicam um lado...",
        "Mas o rastro da terra diz o contrário.",
        "É como se os pés estivessem... invertidos."
    };

    public void Interact()
    {
        if (isRunning || ProgressionManager.Instance.act5FootstepsSeen)
            return;

        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        isRunning = true;

        GameStateManager.SetState(GameState.Thought);

        yield return ThoughtUI.Instance.PlaySequence(Lines);

        GameStateManager.SetState(GameState.Gameplay);

        forestNight2Manager.MarkFootstepsSeen();

        yield return new WaitForSecondsRealtime(0.3f);

        encounterController.TriggerEncounter();

        isRunning = false;
    }
}