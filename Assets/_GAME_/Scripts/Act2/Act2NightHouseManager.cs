using System.Collections;
using UnityEngine;

public class Act2NightHouseManager : MonoBehaviour
{
    [SerializeField] private AudioClip draggingSound;
    [SerializeField] private AudioClip nightHouseAmbience;

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(nightHouseAmbience);

        if (ProgressionManager.Instance.firstNightSleepDone && !ProgressionManager.Instance.firstNightWakeScenePlayed)
        {
            StartCoroutine(NightWakeRoutine());
        }
    }

    private IEnumerator NightWakeRoutine()
    {
        GameStateManager.SetState(GameState.Cutscene);

        yield return new WaitForSecondsRealtime(1f);

            AudioManager.Instance.PlaySFX(draggingSound);

        yield return new WaitForSecondsRealtime(5f);

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