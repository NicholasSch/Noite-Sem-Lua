using System.Collections;
using UnityEngine;

public class BarnToolsInteract : MonoBehaviour, IInteractable
{
    private static readonly string[] Lines =
    {
        "Lucas: Não consigo entrar, mas consigo ver enxadas...",
        "Essas enxadas...",
        "Todas têm o nome 'Dante' entalhado nelas.",
        "Parece que ele fez isso com força suficiente pra rachar a madeira."
    };

    private PlayerController player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void Interact()
    {
        if (TaskManager.Instance.IsCompleted("Barn_Tools"))
        {
            Destroy(gameObject);
            return;
        }

        player.ForceFaceUp();
        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        TaskManager.Instance.CompleteTask("Barn_Tools");
        GameStateManager.SetState(GameState.Thought);

        yield return ThoughtUI.Instance.PlaySequence(Lines);

        GameStateManager.SetState(GameState.Gameplay);
        Destroy(gameObject);
    }
}