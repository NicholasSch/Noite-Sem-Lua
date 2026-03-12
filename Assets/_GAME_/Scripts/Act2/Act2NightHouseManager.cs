using System.Collections;
using UnityEngine;

public class Act2NightHouseManager : MonoBehaviour
{
    [SerializeField] private AudioClip draggingSound;
    [SerializeField] private AudioClip nightHouseAmbience;
    [SerializeField] private AudioClip nightHouseMusic;

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(nightHouseAmbience);

        if (ProgressionManager.Instance.firstNightSleepDone && !ProgressionManager.Instance.firstNightWakeScenePlayed)
        {
            StartCoroutine(NightWakeRoutine());
        }
        else
        {
            AudioManager.Instance.PlayMusic(nightHouseMusic);
        }
    }

    private IEnumerator NightWakeRoutine()
    {
        GameStateManager.SetState(GameState.Cutscene);

        yield return new WaitForSecondsRealtime(1f);

        if (draggingSound != null)
        {
            AudioManager.Instance.PlaySFX(draggingSound);
        }

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

        AudioManager.Instance.PlayMusic(nightHouseMusic);
        GameStateManager.SetState(GameState.Gameplay);
    }
}