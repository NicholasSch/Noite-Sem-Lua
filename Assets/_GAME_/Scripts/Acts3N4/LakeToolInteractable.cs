using System.Collections;
using UnityEngine;

public class LakeTollInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay2Manager farmDay2Manager;
    [SerializeField] private AudioClip coinSound;

    private static readonly string[] FirstLines =
    {
        "<color=#531182>Lucas:</color> Depois daquela visão... eu não sei mais o que é real aqui.",
        "Meu reflexo não se moveu...",
        "E aquela mulher do jornal... Dona Curió...",
        "Ela estava na visão?",
        "Eu preciso ficar atento."
    };

    private static readonly string[] RepeatLines =
    {
        "<color=#531182>Lucas:</color> A água continua estranha."
    };

    private static readonly string[] BlockedLines =
    {
        "<color=#531182>Lucas:</color> Há algo que preciso fazer primeiro."
    };

    public void Interact()
    {
        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        if (!TaskManager.Instance.IsCompleted("Orchard_Care") || !TaskManager.Instance.IsCompleted("Plant_Hope"))
        {
            GameStateManager.SetState(GameState.Thought);
            yield return ThoughtUI.Instance.PlaySequence(BlockedLines);
            GameStateManager.SetState(GameState.Gameplay);
            yield break;
        }

        if (TaskManager.Instance.IsCompleted("Lake_Toll"))
        {
            GameStateManager.SetState(GameState.Thought);
            yield return ThoughtUI.Instance.PlaySequence(RepeatLines);
            GameStateManager.SetState(GameState.Gameplay);
            yield break;
        }

        GameStateManager.SetState(GameState.Cutscene);

        AudioManager.Instance.PlaySFX(coinSound);

        yield return new WaitForSecondsRealtime(1.2f);

        yield return ThoughtUI.Instance.PlaySequence(FirstLines);

        TaskManager.Instance.CompleteTask("Lake_Toll");

        ProgressionManager.Instance.act4Started = true;
        ProgressionManager.Instance.SetJournalPhase(ProgressionManager.JournalPhase.Day2Act4);
        ProgressionManager.Instance.SaveProgress();

        farmDay2Manager.ApplySavedWorldState();

        GameStateManager.SetState(GameState.Gameplay);
    }
}