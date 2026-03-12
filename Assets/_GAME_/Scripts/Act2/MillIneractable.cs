using System.Collections;
using UnityEngine;

public class MillInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip gearSound;

    private static readonly string[] Lines =
    {
        "Lucas: Está parado...",
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
        {
            Destroy(gameObject);
            return;
        }

        player.ForceFaceUp();
        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        TaskManager.Instance.CompleteTask("Mill_Gears");
        AudioManager.Instance.PlaySFX(gearSound);
        GameStateManager.SetState(GameState.Thought);
        yield return new WaitForSecondsRealtime(2f);

        yield return ThoughtUI.Instance.PlaySequence(Lines);

        GameStateManager.SetState(GameState.Gameplay);
        Destroy(gameObject);
    }
}