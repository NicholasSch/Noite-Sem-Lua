using System.Collections;
using UnityEngine;

public class OrchardBushInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay2Manager act3FarmManager;
    [SerializeField] private AudioClip trimmingSound;

    private static readonly string[] Lines =
    {
        "<color=#531182>Lucas:</color> O vovô cuidava dessas árvores como se fossem pessoas.",
        "Dá para sentir que cada uma delas tem uma história."
    };

    public void Interact()
    {
        if (TaskManager.Instance.IsCompleted("Orchard_Care"))
            return;

        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        GameStateManager.SetState(GameState.Thought);

        AudioManager.Instance.PlaySFX(trimmingSound);
        
        yield return new WaitForSecondsRealtime(1.2f);

        yield return ThoughtUI.Instance.PlaySequence(Lines);

        GameStateManager.SetState(GameState.Gameplay);

        act3FarmManager.CompleteOrchardCare();
    }
}