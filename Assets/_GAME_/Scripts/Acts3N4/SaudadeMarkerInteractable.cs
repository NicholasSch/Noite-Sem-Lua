using System.Collections;
using UnityEngine;

public class SaudadeMarkerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay2Manager farmDay2Manager;
    [SerializeField] private AudioClip cleaningSound;

    private static readonly string[] MarkerLines =
    {
        "<color=#531182>Lucas:</color> Debaixo de tanto lodo... ainda tem uma placa.",
        "\"Para Lia: onde quer que você caminhe, este trilho te traz de volta para casa.\"",
        "<color=#531182>Lucas:</color> Ele mapeou a floresta inteira pensando nela...",
        "Cada passo que eu dou aqui parece um rastro do amor que ele sentia."
    };

    public void Interact()
    {
        if (ProgressionManager.Instance.journalPhase != ProgressionManager.JournalPhase.Day2Act4)
            return;
            
        if (TaskManager.Instance.IsCompleted("Trail_Marker"))
            return;

        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        GameStateManager.SetState(GameState.Thought);

        AudioManager.Instance.PlaySFX(cleaningSound);

        yield return new WaitForSecondsRealtime(1.2f);

        yield return ThoughtUI.Instance.PlaySequence(MarkerLines);

        farmDay2Manager.CompleteTrailMarker();

        GameStateManager.SetState(GameState.Gameplay);
    }
}