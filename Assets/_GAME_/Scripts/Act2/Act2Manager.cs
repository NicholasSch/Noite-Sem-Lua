using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Act2Manager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject donaCurioObject;
    [SerializeField] private GameObject barnToolsObject;
    [SerializeField] private GameObject millInteractableObject;
    [SerializeField] private NPCController corpoSecoPrefab;
    [SerializeField] private PlayerController player;

    [Header("UIS")]
    [SerializeField] private GameUI gameUI;
    [SerializeField] private NarrationUI narrationUI;
    [SerializeField] private TitleUI titlePrefab;
    


    [Header("Cutscene Points")]
    [SerializeField] private Transform playerLookPosition;
    [SerializeField] private Transform corpoSecoSpawnPoint;
    [SerializeField] private Transform corpoSecoLookDir;
    [SerializeField] private Transform horizonLookTarget;
    [SerializeField] private Transform millLookTarget;

    [Header("Audio")]
    [SerializeField] private AudioClip dayFarmMusic;
    [SerializeField] private AudioClip dayFarmAmbience;
    [SerializeField] private AudioClip draggingSound;
    [SerializeField] private AudioClip windBurstSound;
    [SerializeField] private AudioClip nightFarmAmbience;
    [SerializeField] private string corpoSecoPointAnimation = "Anim_CorpoSeco_Point";

    private void Start()
    {
        CleanupSceneObjects();
        StartCoroutine(SceneFlowRoutine());

        if (ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Day)
        {
            AudioManager.Instance.PlayAmbient(dayFarmAmbience);
            AudioManager.Instance.PlayMusic(dayFarmMusic);
        }
        else if (ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Night &&
                 ProgressionManager.Instance.firstNightTitlePlayed)
        {
            AudioManager.Instance.PlayAmbient(nightFarmAmbience);
        }
    }

    private void CleanupSceneObjects()
    {
        if (ProgressionManager.Instance.HasTalkedToNpc("CucaDisguised") && donaCurioObject != null)
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

        if (sceneName == "Farm_Day_1" &&
            day == 1 &&
            period == ProgressionManager.DayPeriod.Day &&
            !ProgressionManager.Instance.farmIntroPlayed)
        {
            yield return PlayFarmIntro();
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

    private IEnumerator PlayFirstNightOutsideScene()
    {
        GameStateManager.SetState(GameState.Cutscene);

        if (gameUI != null)
        {
            gameUI.gameObject.SetActive(false);
        }

        AudioManager.Instance.PlayAmbient(nightFarmAmbience);

        if (millLookTarget != null)
        {
            player.LookAtTarget(millLookTarget);
        }

        NPCController corpoSeco = Instantiate(corpoSecoPrefab, corpoSecoSpawnPoint.position, Quaternion.identity);
        corpoSeco.PlayIdle();
        corpoSeco.LookAtTarget(player.transform);

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


        corpoSeco.LookAtTarget(corpoSecoLookDir);

        yield return new WaitForSecondsRealtime(0.8f);

        corpoSeco.PlayAnimationState(corpoSecoPointAnimation);

        yield return new WaitForSecondsRealtime(2f);

        corpoSeco.ResetToIdle();

        yield return new WaitForSecondsRealtime(0.2f);

        if (horizonLookTarget != null)
        {
            player.LookAtTarget(horizonLookTarget);
        }


        if (windBurstSound != null)
        {
            AudioManager.Instance.PlaySFX(windBurstSound);
        }

        yield return new WaitForSecondsRealtime(0.7f);

        Destroy(corpoSeco.gameObject);

        yield return new WaitForSecondsRealtime(0.5f);

        TitleUI titleInstance = Instantiate(titlePrefab);
        yield return titleInstance.Play();

        ProgressionManager.Instance.firstNightTitlePlayed = true;
        ProgressionManager.Instance.NextDay();
        ProgressionManager.Instance.SetPeriod(ProgressionManager.DayPeriod.Day);

        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.House,
            SceneRouteManager.EntryPoint.Default
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);

        yield return narrationUI.ShowTextRoutine("", route.SceneName);
    }
}