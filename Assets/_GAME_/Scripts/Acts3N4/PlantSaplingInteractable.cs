using System.Collections;
using UnityEngine;

public class PlantSaplingInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay2Manager farmDay2Manager;
    [SerializeField] private AudioClip PlantingSound;

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

        AudioManager.Instance.PlaySFX(PlantingSound);

        yield return new WaitForSecondsRealtime(1.2f);

        yield return ThoughtUI.Instance.PlaySequence(PlantLines);

        farmDay2Manager.CompletePlantHope();

        GameStateManager.SetState(GameState.Gameplay);
    }
}