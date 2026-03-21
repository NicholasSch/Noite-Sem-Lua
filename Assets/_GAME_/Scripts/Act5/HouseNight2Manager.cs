using System.Collections;
using UnityEngine;

public class HouseNight2Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip nightHouseAmbience;
    [SerializeField] private AudioClip chestOpenSound;
    [SerializeField] private AudioClip paperPickupSound;
    
    [Header("Sprites")]
    [SerializeField] private GameObject chestClosedObject;
    [SerializeField] private GameObject chestOpenFullObject;
    [SerializeField] private GameObject chestOpenEmptyObject;

    [Header("Dependencies")]
    [SerializeField] private GameUI gameUI;

    private static readonly string[] ChestLines =
    {
        "<color=#531182>Lucas:</color> Não tem lanterna aqui...",
        "Mas tem um pacote embrulhado em papel pardo.",
    };

        private static readonly string[] PaperLine =
    {
        "“Para quem guarda as trilhas, o fumo é o melhor cumprimento.”",
    };

        private static readonly string[] SecondChestLines =
    {
        "<color=#531182>Lucas:</color> O vovô guardava isso como se fosse um tesouro... fumo de rolo?",
        "Ele sempre dizia que a mata tem seus donos e que é bom ser educado com eles.",
        "Melhor eu levar. Do jeito que as coisas andam aqui, qualquer cuidado é pouco."
    };

        private static readonly string[] NighIntroLines =
    {
        "<color=#531182>Lucas:</color> Não... eu devo ter deixado o caderno na feira.",
        "Sem as instruções do vovô pra amanhã, eu tô perdido.",
        "Preciso voltar lá agora, antes que a neblina feche o caminho de vez.",
        "Antes disso, vou procurar uma lanterna extra no quarto dele."
    };

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
        chestClosedObject.SetActive(!ProgressionManager.Instance.act5TobaccoFound);
        chestOpenEmptyObject.SetActive(ProgressionManager.Instance.act5TobaccoFound);

    }

    private IEnumerator PlayAct5Intro()
    {
        GameStateManager.SetState(GameState.Cutscene);
        gameUI.gameObject.SetActive(false);

        yield return ThoughtUI.Instance.PlaySequence(NighIntroLines);

        ProgressionManager.Instance.act5NightIntroPlayed = true;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
    }

    public void PlayChestOpeningSequence()
    {
        StartCoroutine(ChestOpeningSequence());
    }
    private IEnumerator ChestOpeningSequence()
    {

    GameStateManager.SetState(GameState.Thought);

    AudioManager.Instance.PlaySFX(chestOpenSound);

    chestClosedObject.SetActive(false);
    chestOpenFullObject.SetActive(true);
    chestOpenEmptyObject.SetActive(false);

    yield return new WaitForSecondsRealtime(0.8f);

    yield return ThoughtUI.Instance.PlaySequence(ChestLines);

    AudioManager.Instance.PlaySFX(paperPickupSound);

    
    yield return new WaitForSecondsRealtime(0.5f);

    yield return ThoughtUI.Instance.PlaySequence(PaperLine);

    yield return new WaitForSecondsRealtime(0.4f);

    
    yield return ThoughtUI.Instance.PlaySequence(SecondChestLines);

    ProgressionManager.Instance.act5TobaccoFound = true;
    ProgressionManager.Instance.SaveProgress();

    chestClosedObject.SetActive(false);
    chestOpenFullObject.SetActive(false);
    chestOpenEmptyObject.SetActive(true);

    GameStateManager.SetState(GameState.Gameplay);
    }

}