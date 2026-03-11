using System.Collections;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private NPCDialogue dialogue;
    [SerializeField] private NPCController controller;
    [SerializeField] private Transform walkTarget;
    [SerializeField] private GameObject objectToDisableAfterConversation;

    private static readonly string[] Lines =
    {
        "<color=#531182>Lucas:</color> Que mulher estranha",
        "Este lugar é estranho",
        "Bom, eu deveria fazer o que o caderno manda agora"
    };

    private bool talking;
    private PlayerController player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void Interact()
    {
        if (talking)
            return;

        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        talking = true;

        GameStateManager.SetState(GameState.Cutscene);
        player.ForceFaceUp();

        yield return dialogue.StartDialogue();

        ProgressionManager.Instance.talkedToDonaCurio = true;
        ProgressionManager.Instance.SaveProgress();

        if (walkTarget != null)
        {
            StartCoroutine(controller.WalkTo(walkTarget));
        }

        yield return ThoughtUI.Instance.PlaySequence(Lines);

        if (objectToDisableAfterConversation != null)
        {
            objectToDisableAfterConversation.SetActive(false);
        }

        GameStateManager.SetState(GameState.Gameplay);
        talking = false;
    }
}