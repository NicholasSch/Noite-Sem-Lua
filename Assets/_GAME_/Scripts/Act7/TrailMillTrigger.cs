using System.Collections;
using UnityEngine;

public class TrailMillTrigger : MonoBehaviour
{
    private bool isRunning;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!ProgressionManager.Instance.act7MorningIntroPlayed)
            return;

        if (ProgressionManager.Instance.act7TrailObserved)
            return;

        if (isRunning)
            return;

        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        isRunning = true;
        GameStateManager.SetState(GameState.Thought);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> Esse rastro...",
            "É como se algo tivesse passado por aqui e queimado a vida sem usar fogo.",
            "O vovô escreveu sobre o 'excesso de mágoa' matando a terra.",
            "Dá para sentir a mágoa no ar."
        });

        ProgressionManager.Instance.act7TrailObserved = true;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;
    }
}