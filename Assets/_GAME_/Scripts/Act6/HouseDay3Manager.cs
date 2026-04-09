using System.Collections;
using UnityEngine;

public class HouseDay3Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dayHouseAmbience;
    [SerializeField] private AudioClip leafHitSound;

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(dayHouseAmbience);
        StartCoroutine(SceneFlowRoutine());
    }


    private IEnumerator SceneFlowRoutine()
    {
        if (ProgressionManager.Instance.currentDay == 3 &&
            ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Day &&
            !ProgressionManager.Instance.act6MorningIntroPlayed)
        {
            yield return PlayDay3MorningIntro();
        }
    }

    private IEnumerator PlayDay3MorningIntro()
    {
        GameStateManager.SetState(GameState.Thought);

        AudioManager.Instance.PlaySFX(leafHitSound);
        yield return new WaitForSecondsRealtime(1f);

        string[] lines =
        {
            "<color=#531182>Lucas:</color> ...",
            "Folhas batendo na janela.",
            "Parece que a casa quer me tirar da cama à força."
        };

        yield return ThoughtUI.Instance.PlaySequence(lines);

        ProgressionManager.Instance.act6MorningIntroPlayed = true;
        ProgressionManager.Instance.SetJournalPhase(ProgressionManager.JournalPhase.Day3Act6);
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
    }
}