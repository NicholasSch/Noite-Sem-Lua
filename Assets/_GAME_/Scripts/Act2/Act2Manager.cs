using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Act2Manager : MonoBehaviour
{
    //Scene objects
    [SerializeField] private GameObject donaCurioObject;
    [SerializeField] private GameObject barnToolsObject;
    [SerializeField] private GameObject millInteractableObject;

    //References
    [SerializeField] private GameUI gameUI;
    [SerializeField] private PlayerController player;
    [SerializeField] private NarrationUI narrationUI;

    //Prefabs
    [SerializeField] private TitleUI titlePrefab;
    [SerializeField] private CorpoSecoController corpoSecoPrefab;

    //Night Scene Positions
    [SerializeField] private Transform playerLookPosition;
    [SerializeField] private Transform corpoSecoSpawnPoint;
    [SerializeField] private Transform horizonLookTarget;
    [SerializeField] private Transform millLookTarget;

    //Audio
    [SerializeField] private AudioClip draggingSound;
    [SerializeField] private AudioClip windBurstSound;
    [SerializeField] private AudioClip nightAmbience;

    private void Start()
    {
        CleanupSceneObjects();
        StartCoroutine(SceneFlowRoutine());
    }

    private void CleanupSceneObjects()
    {
        if (ProgressionManager.Instance.talkedToDonaCurio && donaCurioObject != null)
        {
            Destroy(donaCurioObject);
        }

        if (TaskManager.Instance.IsCompleted("Barn_Tools") && barnToolsObject != null)
        {
            Destroy(barnToolsObject);
        }

        if (TaskManager.Instance.IsCompleted("Mill_Gears") && millInteractableObject != null)
        {
            Destroy(millInteractableObject);
        }
    }

    private IEnumerator SceneFlowRoutine()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int day = ProgressionManager.Instance.currentDay;
        ProgressionManager.DayPeriod period = ProgressionManager.Instance.currentPeriod;

        if (sceneName == "Farm_Day_1" && day == 1 && period == ProgressionManager.DayPeriod.Day && !ProgressionManager.Instance.farmIntroPlayed)
        {
            yield return PlayFarmIntro();
            yield break;
        }

        if (sceneName == "House_Night_1" &&
            day == 1 &&
            period == ProgressionManager.DayPeriod.Night &&
            ProgressionManager.Instance.firstNightSleepDone &&
            !ProgressionManager.Instance.firstNightWakeScenePlayed)
        {
            yield return PlayHouseNightWakeScene();
            yield break;
        }

        if (sceneName == "Farm_Night_1" &&
            day == 1 &&
            period == ProgressionManager.DayPeriod.Night &&
            ProgressionManager.Instance.firstNightWakeScenePlayed &&
            !ProgressionManager.Instance.firstNightTitlePlayed)
        {
            yield return PlayFirstNightOutsideScene();
        }
    }

    private IEnumerator PlayFarmIntro()
    {
        yield return new WaitForSecondsRealtime(1f);

        string[] lines =
        {
            "<color=#531182>Lucas:</color> Droga... o motor ferveu bem na entrada.",
            "Pelo menos eu já trouxe o caderno do vovô.",
            "Parece que vou ter tempo de sobra para ler enquanto esse ferro velho esfria."
        };

        yield return ThoughtUI.Instance.PlaySequence(lines);

        ProgressionManager.Instance.farmIntroPlayed = true;
        ProgressionManager.Instance.SaveProgress();
    }

    private IEnumerator PlayHouseNightWakeScene()
    {
        GameStateManager.SetState(GameState.Cutscene);

        yield return new WaitForSecondsRealtime(1.2f);

        if (draggingSound != null)
        {
            AudioManager.Instance.PlaySFX(draggingSound);
        }

        string[] lines =
        {
            "<color=#531182>Lucas:</color> ...Que som foi esse?",
            "Parece que tem alguma coisa sendo arrastada lá fora."
        };

        yield return ThoughtUI.Instance.PlaySequence(lines);

        ProgressionManager.Instance.firstNightWakeScenePlayed = true;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
    }

    private IEnumerator PlayFirstNightOutsideScene()
    {
        GameStateManager.SetState(GameState.Cutscene);

        if (gameUI != null)
        {
            gameUI.gameObject.SetActive(false);
        }

        if (nightAmbience != null)
        {
            AudioManager.Instance.PlayAmbient(nightAmbience);
        }

        if (millLookTarget != null)
        {
            player.LookAtTarget(millLookTarget);
        }


        CorpoSecoController corpoSeco = Instantiate(corpoSecoPrefab, corpoSecoSpawnPoint.position, Quaternion.identity);
        corpoSeco.PlayIdle();
        corpoSeco.LookAt(player.transform);

        string[] apparitionLines =
        {
            "<color=#531182>Lucas:</color> ...",
            "Tem alguém perto do moinho.",
            "Não... isso não é alguém."
        };

        yield return ThoughtUI.Instance.PlaySequence(apparitionLines);

        if (playerLookPosition != null)
        {
            yield return player.MoveTo(playerLookPosition.position, 2f);
        }

        player.LookAtTarget(corpoSeco.transform);

        yield return new WaitForSecondsRealtime(0.8f);

        corpoSeco.PlayPoint();

        yield return new WaitForSecondsRealtime(1f);

        if (horizonLookTarget != null)
        {
            player.LookAtTarget(horizonLookTarget);
        }

        yield return new WaitForSecondsRealtime(2f);

        if (windBurstSound != null)
        {
            AudioManager.Instance.PlaySFX(windBurstSound);
        }

        yield return corpoSeco.Disappear();

        TitleUI titleInstance = Instantiate(titlePrefab);
        titleInstance.Setup("NOITE SEM LUA", "As Sombras da Promessa");
        yield return titleInstance.Play();

        ProgressionManager.Instance.firstNightTitlePlayed = true;
        ProgressionManager.Instance.SaveProgress();

        if (gameUI != null)
        {
            gameUI.gameObject.SetActive(true);
        }

        GameStateManager.SetState(GameState.Gameplay);
    }
}