using System.Collections;
using UnityEngine;

public class HouseNight5Manager : MonoBehaviour
{
    [SerializeField] private AudioClip nightAmbience;
    [SerializeField] private AudioClip nightMusic;

    private void Start()
    {
        if (ProgressionManager.Instance.act9IntroPlayed)
            return;

        if (ProgressionManager.Instance.journalPhase == ProgressionManager.JournalPhase.Day5Act9)
            return;

        StartCoroutine(IntroRoutine());
    }

    private IEnumerator IntroRoutine()
    {
        GameStateManager.SetState(GameState.Thought);

            AudioManager.Instance.PlayAmbient(nightAmbience);
            AudioManager.Instance.PlayMusic(nightMusic, 2f);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
        "<color=#531182>Lucas:</color> ...Então é isso.",
        "Não tem mais pra onde correr.",
        "Se ele quer terminar isso... eu também quero."
        });


        ProgressionManager.Instance.SetJournalPhase(ProgressionManager.JournalPhase.Day5Act9);
        ProgressionManager.Instance.act9IntroPlayed = true ;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
    }
}