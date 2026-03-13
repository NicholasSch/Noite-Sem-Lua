using System.Collections;
using UnityEngine;

public class Act3Manager : MonoBehaviour
{
    [SerializeField] private AudioClip dayHouseAmbience;
    [SerializeField] private AudioClip dayHouseMusic;

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(dayHouseAmbience);
        AudioManager.Instance.PlayMusic(dayHouseMusic);

        StartCoroutine(SceneFlowRoutine());
    }

    private IEnumerator SceneFlowRoutine()
    {
        if (ProgressionManager.Instance.currentDay == 2 &&
            ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Day &&
            !ProgressionManager.Instance.act3MorningIntroPlayed)
        {
            yield return PlayDay2MorningIntro();
        }
    }

    private IEnumerator PlayDay2MorningIntro()
    {
        GameStateManager.SetState(GameState.Thought);

        string[] lines =
        {
            "<color=#531182>Lucas:</color> O sol já está alto...",
            "O caderno está diferente hoje.",
            "As palavras parecem mais... vivas."
        };

        yield return ThoughtUI.Instance.PlaySequence(lines);

        ProgressionManager.Instance.act3MorningIntroPlayed = true;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
    }
}