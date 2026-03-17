using System.Collections;
using UnityEngine;

public class BarnToolsInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay1Manager farmDay1Manager;
    [SerializeField] private AudioClip barnDoorSound;

    private static readonly string[] Lines =
    {
        "<color=#531182>Lucas:</color> Não consigo entrar, mas consigo ver enxadas...",
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
            return;

        player.ForceFaceUp();
        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        GameStateManager.SetState(GameState.Thought);

        AudioManager.Instance.PlaySFX(barnDoorSound);

        yield return new WaitForSecondsRealtime(1.2f);

        yield return ThoughtUI.Instance.PlaySequence(Lines);

        farmDay1Manager.CompleteBarnTools();

        GameStateManager.SetState(GameState.Gameplay);
    }
}