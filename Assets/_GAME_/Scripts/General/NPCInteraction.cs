using UnityEngine;
using System.Collections;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogue;
    public NPCController controller;

    public Transform walkTarget;

    private string[] lines =
    {
        "<color=#531182>Lucas:</color>Que mulher estranha",
        "Este lugar é estranho",
        "Bom eu deveria fazer o que o caderno manda agora"
    };

    bool talking;

    PlayerController player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void Interact()
    {
        if (talking) return;

        StartCoroutine(InteractionRoutine());
    }

    IEnumerator InteractionRoutine()
    {
        talking = true;

        GameStateManager.CurrentState = GameState.Cutscene;

        player.ForceFaceUp();


        yield return dialogue.StartDialogue();

        if (walkTarget != null)
            StartCoroutine(controller.WalkTo(walkTarget));

            yield return ThoughtUI.Instance.PlaySequence(lines);
        GameStateManager.CurrentState = GameState.Gameplay;

        talking = false;
    }
}