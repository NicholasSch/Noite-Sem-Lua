using System.Collections;
using UnityEngine;

public class MillInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay1Manager farmDay1Manager;
    [SerializeField] private AudioClip gearSound;

    private static readonly string[] Lines =
    {
        "<color=#531182>Lucas:</color> Está parado...",
        "Mas sinto que o moinho está esperando por algo.",
        "Ou por alguém."
    };

    private PlayerController player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void Interact()
    {
        if (TaskManager.Instance.IsCompleted("Mill_Gears"))
            return;

        player.ForceFaceUp();
        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        GameStateManager.SetState(GameState.Thought);

        AudioManager.Instance.PlaySFX(gearSound);

        yield return new WaitForSecondsRealtime(1.2f);

        yield return ThoughtUI.Instance.PlaySequence(Lines);

        farmDay1Manager.CompleteMillGears();

        GameStateManager.SetState(GameState.Gameplay);
    }
}