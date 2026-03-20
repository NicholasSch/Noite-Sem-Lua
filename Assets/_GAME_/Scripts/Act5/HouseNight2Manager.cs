using System.Collections;
using UnityEngine;

public class HouseNight2Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip nightHouseAmbience;

    [Header("Dependencies")]
    [SerializeField] private GameUI gameUI;

    [Header("Objects")]
    [SerializeField] private GameObject tobaccoPackageInteractable;

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(nightHouseAmbience);

        if (ProgressionManager.Instance.currentDay == 2 &&
            ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Night &&
            !ProgressionManager.Instance.act5NightIntroPlayed)
        {
            StartCoroutine(PlayAct5Intro());
            return;
        }

        gameUI.gameObject.SetActive(!ProgressionManager.Instance.act4HideGameUI);
        tobaccoPackageInteractable.SetActive(!ProgressionManager.Instance.act5TobaccoFound);
    }

    private IEnumerator PlayAct5Intro()
    {
        GameStateManager.SetState(GameState.Cutscene);
        gameUI.gameObject.SetActive(false);

        string[] lines =
        {
            "<color=#531182>Lucas:</color> Não... eu devo ter deixado o caderno na feira.",
            "Sem as instruções do vovô pra amanhã, eu tô perdido.",
            "Preciso voltar lá agora, antes que a neblina feche o caminho de vez.",
            "Mas antes... vou procurar uma lanterna no quarto dele."
        };

        yield return ThoughtUI.Instance.PlaySequence(lines);

        ProgressionManager.Instance.act5NightIntroPlayed = true;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
    }
}