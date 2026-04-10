using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FarmNight1Manager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private NPCController corpoSecoPrefab;
    [SerializeField] private PlayerController player;

    [Header("UI")]
    [SerializeField] private TitleUI titlePrefab;

    [Header("Cutscene Points")]
    [SerializeField] private Transform corpoSecoSpawnPoint;
    [SerializeField] private Transform playerWalkPosition;
    [SerializeField] private Transform corpoSecoPointDir;
    [SerializeField] private Transform PlayerLookTarget;

    [Header("Audio")]
    [SerializeField] private AudioClip nightFarmAmbience;
    [SerializeField] private AudioClip windBurstSound;
    [SerializeField] private string corpoSecoPointAnimation = "Pointing";

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(nightFarmAmbience);

        if (ProgressionManager.Instance.currentDay == 1 &&
            ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Night &&
            ProgressionManager.Instance.firstNightWakeScenePlayed &&
            !ProgressionManager.Instance.firstNightTitlePlayed)
        {
            StartCoroutine(PlayFirstNightOutsideScene());
        }
    }

    private IEnumerator PlayFirstNightOutsideScene()
    {
        GameStateManager.SetState(GameState.Cutscene);

        GameUI.Instance.gameObject.SetActive(false);

        player.LookAtTarget(corpoSecoSpawnPoint);

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

        yield return player.MoveTo(playerWalkPosition.position, 2f);

        corpoSeco.LookAtTarget(corpoSecoPointDir);

        yield return new WaitForSecondsRealtime(0.8f);

        corpoSeco.PlayAnimationState(corpoSecoPointAnimation);

        yield return new WaitForSecondsRealtime(2f);

        corpoSeco.ResetToIdle();

        yield return new WaitForSecondsRealtime(0.2f);

        player.LookAtTarget(PlayerLookTarget);

        AudioManager.Instance.PlaySFX(windBurstSound);

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

        yield return NarrationUI.Instance.ShowTextRoutine("", route.SceneName);
    }
}