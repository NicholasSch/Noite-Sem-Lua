using System.Collections;
using UnityEngine;

public class SleepInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private NarrationUI narrationUI;
    [SerializeField] private string sleepText = "<color=#531182>Lucas:</color> Acho melhor descansar um pouco.";
    [SerializeField] private string blockedSleepText = "<color=#531182>Lucas:</color> Ainda não. Tenho coisas para resolver antes de dormir.";
    [SerializeField] private SceneRouteManager.EntryPoint sleepWakeEntryPoint = SceneRouteManager.EntryPoint.Default;

    public void Interact()
    {
        StartCoroutine(SleepRoutine());
    }

    private IEnumerator SleepRoutine()
    {
        if (ProgressionManager.Instance.currentDay == 1 && !ProgressionManager.Instance.porchScenePlayed)
        {
            yield return narrationUI.ShowTextRoutine(blockedSleepText);
            yield break;
        }

        GameStateManager.SetState(GameState.Cutscene);
        ProgressionManager.Instance.SetPeriod(ProgressionManager.DayPeriod.Night);

        if (ProgressionManager.Instance.currentDay == 1 && !ProgressionManager.Instance.firstNightSleepDone)
        {
            ProgressionManager.Instance.firstNightSleepDone = true;
        }

        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.House,
            sleepWakeEntryPoint
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);
        ProgressionManager.Instance.SaveProgress();

        yield return narrationUI.ShowTextRoutine(sleepText, route.SceneName);
    }
}