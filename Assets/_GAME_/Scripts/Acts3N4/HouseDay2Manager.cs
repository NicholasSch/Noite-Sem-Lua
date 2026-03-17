using System.Collections;
using UnityEngine;

public class HouseDay2Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dayHouseAmbience;

    [Header("Objects")]
    [SerializeField] private GameObject radioObject;

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(dayHouseAmbience);
        ApplySavedWorldState();
        StartCoroutine(SceneFlowRoutine());
    }

    public void ApplySavedWorldState()
    {   
        radioObject.SetActive(ProgressionManager.Instance.act4RadioBought);
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
        ProgressionManager.Instance.SetJournalPhase(ProgressionManager.JournalPhase.Day2Act3);
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
        
    }
}