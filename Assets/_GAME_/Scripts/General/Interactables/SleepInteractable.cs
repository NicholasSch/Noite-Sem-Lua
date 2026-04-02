using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SleepInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string sleepText = "<color=#531182>Lucas:</color> Acho melhor descansar um pouco.";
    [SerializeField] private string blockedSleepText = "<color=#531182>Lucas:</color> Ainda não. Tenho coisas para resolver antes de dormir.";
    [SerializeField] private SceneRouteManager.EntryPoint sleepWakeEntryPoint = SceneRouteManager.EntryPoint.Default;

    public void Interact()
    {
        StartCoroutine(SleepChecker());
    }

    private IEnumerator SleepChecker()
    {
        bool act6TasksDone =
            TaskManager.Instance.IsCompleted("Sentinel_Thirst") &&
            TaskManager.Instance.IsCompleted("House_Whistle");

        if ((ProgressionManager.Instance.currentDay == 1 && !ProgressionManager.Instance.porchScenePlayed) ||
            (ProgressionManager.Instance.currentDay == 2 && !ProgressionManager.Instance.act5JournalRecovered) ||
            (ProgressionManager.Instance.currentDay == 3 &&
             ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Day &&
             !act6TasksDone))
        {
            yield return NarrationUI.Instance.ShowTextRoutine(blockedSleepText);
            yield break;
        }
        else if (ProgressionManager.Instance.currentDay == 1 &&
                 ProgressionManager.Instance.porchScenePlayed &&
                 !ProgressionManager.Instance.firstNightSleepDone)
        {
            ProgressionManager.Instance.firstNightSleepDone = true;
            ProgressionManager.Instance.SetPeriod(ProgressionManager.DayPeriod.Night);
        }
        else if (ProgressionManager.Instance.currentDay == 3 &&
                 ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Day &&
                 act6TasksDone &&
                 !ProgressionManager.Instance.act6NightStarted)
        {
            ProgressionManager.Instance.act6NightStarted = true;
            ProgressionManager.Instance.SetPeriod(ProgressionManager.DayPeriod.Night);
            ProgressionManager.Instance.SaveProgress();
        }
        else
        {
            ProgressionManager.Instance.NextDay();
        }

        StartCoroutine(SleepRoutine());
    }

    private IEnumerator SleepRoutine()
    {

        AudioManager.Instance.StopMusic();
        AudioManager.Instance.StopAmbient();
        GameStateManager.SetState(GameState.Cutscene);

        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.House,
            sleepWakeEntryPoint
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);
        ProgressionManager.Instance.SaveProgress();

        yield return NarrationUI.Instance.ShowTextRoutine(sleepText, route.SceneName);
    }
}