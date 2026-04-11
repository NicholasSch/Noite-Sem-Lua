using System.Collections;
using UnityEngine;

public class HouseDay5Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dayHouseAmbience;

    private void Start()
    {
        ProgressionManager.Instance.SetJournalPhase(ProgressionManager.JournalPhase.Day5Act8);
        AudioManager.Instance.PlayAmbient(dayHouseAmbience);
        StartCoroutine(SceneFlowRoutine());
    }

    private IEnumerator SceneFlowRoutine()
    {
        if (ProgressionManager.Instance.currentDay == 5 &&
            ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Day &&
            !ProgressionManager.Instance.act8MorningIntroPlayed)
        {
            yield return PlayMorningIntro();
        }
    }

    private IEnumerator PlayMorningIntro()
    {
        GameStateManager.SetState(GameState.Thought);
        GameUI.Instance.gameObject.SetActive(false);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> Eu dormi por um dia inteiro...",
            "O relógio ficou aqui, como se quisesse me lembrar do que está em jogo.",
            "A letra do vovô está pior hoje.",
            "Como se ele já estivesse escrevendo sem forças."
        });

        ProgressionManager.Instance.act8MorningIntroPlayed = true;
        ProgressionManager.Instance.SaveProgress();

        GameUI.Instance.gameObject.SetActive(true);
        GameStateManager.SetState(GameState.Gameplay);
    }
}