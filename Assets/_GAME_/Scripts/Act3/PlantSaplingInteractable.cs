using System.Collections;
using UnityEngine;

public class PlantSaplingInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Act3FarmManager act3FarmManager;

    private static readonly string[] PlantLines =
    {
        "<color=#531182>Lucas:</color> Vou plantar isso aqui... por eles.",
        "É o mínimo que posso fazer para manter a promessa do vovô viva."
    };


    public void Interact()
    {
        if (TaskManager.Instance.IsCompleted("Plant_Hope"))
            return;

        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {

        GameStateManager.SetState(GameState.Thought);

        yield return ThoughtUI.Instance.PlaySequence(PlantLines);

        act3FarmManager.CompletePlantHope();

        GameStateManager.SetState(GameState.Gameplay);
    }
}