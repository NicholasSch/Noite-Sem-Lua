using UnityEngine;
using System.Collections;

public class MillInteractable : MonoBehaviour, IInteractable
{
    public AudioClip gearSound;
    public AudioSource audioSource;

    PlayerController player;

    string[] lines =
    {
        "Lucas: Está parado...",
        "Mas sinto que o moinho está esperando por algo.",
        "Ou por alguém."
    };

        void Start()
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

    IEnumerator InteractionRoutine()
    {
        TaskManager.Instance.CompleteTask("Mill_Gears");

        audioSource.PlayOneShot(gearSound);

        GameStateManager.CurrentState = GameState.Dialogue;

        yield return ThoughtUI.Instance.PlaySequence(lines);

        GameStateManager.CurrentState = GameState.Gameplay;

        Destroy(gameObject);
    }
}