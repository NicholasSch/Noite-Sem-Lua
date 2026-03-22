using System.Collections;
using UnityEngine;

public class CurupiraFootprintsInteractable : MonoBehaviour
{
    [SerializeField] private CurupiraEncounterController encounterController;

    private bool triggered = false;

    private static readonly string[] Lines =
    {
        "<color=#531182>Lucas:</color> Que estranho...",
        "Quem conseguiria caminhar assim?",
        "As pegadas indicam um lado...",
        "Mas o rastro da terra diz o contrário.",
        "É como se os pés estivessem... invertidos.", 
        "E quanta força nessas pegadas."
    };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;
        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        GameStateManager.SetState(GameState.Thought);

        yield return ThoughtUI.Instance.PlaySequence(Lines);

        GameStateManager.SetState(GameState.Gameplay);

        yield return new WaitForSecondsRealtime(0.3f);

        triggered = true;

        encounterController.TriggerEncounter();
    }
}