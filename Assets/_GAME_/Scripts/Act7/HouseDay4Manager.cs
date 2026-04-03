using System.Collections;
using UnityEngine;

public class HouseDay4Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dayHouseAmbience;

    private void Start()
    {
        ProgressionManager.Instance.SetJournalPhase(ProgressionManager.JournalPhase.Day4Act7);
        AudioManager.Instance.PlayAmbient(dayHouseAmbience);
        StartCoroutine(SceneFlowRoutine());
    }

    private IEnumerator SceneFlowRoutine()
    {
        if (ProgressionManager.Instance.currentDay == 4 &&
            ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Day &&
            !ProgressionManager.Instance.act7MorningIntroPlayed)
        {
            yield return PlayAct7MorningIntro();
        }
    }

    private IEnumerator PlayAct7MorningIntro()
    {
        GameStateManager.SetState(GameState.Thought);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> Eu não dormi.",
            "Minha mão ainda está tremendo.",
            "O assobio... a visão do pacto... ainda estão aqui.",
            "Mas o bilhete do vovô também está."
        });

        ProgressionManager.Instance.act7MorningIntroPlayed = true;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
    }
}