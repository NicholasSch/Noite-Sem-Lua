using System.Collections;
using UnityEngine;

public class LakeTollInteractable : MonoBehaviour, IInteractable
{
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

    public void Interact()
    {
        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
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
        GameStateManager.SetState(GameState.Gameplay);
    }
}