using System.Collections;
using UnityEngine;

public class Act2NightHouseManager : MonoBehaviour
{
    [SerializeField] private AudioClip draggingSound;
    [SerializeField] private AudioClip nightAmbience;

    private void Start()
    {
        if (!ProgressionManager.Instance.firstNightSleepDone)
            return;

        if (ProgressionManager.Instance.firstNightWakeScenePlayed)
            return;

        StartCoroutine(NightWakeRoutine());
    }

    private IEnumerator NightWakeRoutine()
    {
        GameStateManager.SetState(GameState.Cutscene);

        if (nightAmbience != null)
        {
            AudioManager.Instance.PlayAmbient(nightAmbience);
        }

        yield return new WaitForSecondsRealtime(1f);

        if (draggingSound != null)
        {
            AudioManager.Instance.PlaySFX(draggingSound);
        }

        string[] lines =
        {
            "<color=#531182>Lucas:</color> ...",
            "Tem algo sendo arrastado lá fora.",
            "Esse som veio do lado do moinho?"
        };

        yield return ThoughtUI.Instance.PlaySequence(lines);

        ProgressionManager.Instance.firstNightWakeScenePlayed = true;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
    }
}