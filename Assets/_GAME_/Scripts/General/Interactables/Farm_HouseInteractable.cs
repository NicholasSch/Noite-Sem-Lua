using System.Collections;
using UnityEngine;

public class Farm_HouseInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip doorSound;

    public void Interact()
    {   
        if (ProgressionManager.Instance.currentDay == 2 && ProgressionManager.Instance.act4CurioEncounterPlayed)
        {
           ProgressionManager.Instance.SetPeriod(ProgressionManager.DayPeriod.Night);
        }
        StartCoroutine(EnterHouseRoutine());
    }

    private IEnumerator EnterHouseRoutine()
    {
        AudioManager.Instance.PlaySFX(doorSound);

        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.House,
            SceneRouteManager.EntryPoint.FromFarm
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);

        yield return NarrationUI.Instance.ShowTextRoutine("", route.SceneName);
    }
}