using UnityEngine;
using System.Collections;

public class BarnToolsInteract : MonoBehaviour, IInteractable
{

    PlayerController player;
    string[] lines =
    {
        "Lucas: Não consigo entrar, mas consigo ver enxadas...",
        "Essas enxadas...",
        "Todas têm o nome 'Dante' entalhado nelas.",
        "Parece que ele fez isso com força suficiente pra rachar a madeira."
    };

        void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void Interact()
    {
        if (TaskManager.Instance.IsCompleted("Barn_Tools"))
            return;
        player.ForceFaceUp();
        StartCoroutine(InteractionRoutine());
    }

    IEnumerator InteractionRoutine()
    {
        TaskManager.Instance.CompleteTask("Barn_Tools");

        GameStateManager.CurrentState = GameState.Dialogue;

        yield return ThoughtUI.Instance.PlaySequence(lines);

        GameStateManager.CurrentState = GameState.Gameplay;

        Destroy(gameObject);
    }
}