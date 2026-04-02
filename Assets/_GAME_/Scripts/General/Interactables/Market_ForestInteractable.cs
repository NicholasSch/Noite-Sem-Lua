using System.Collections;
using UnityEngine;

public class Market_ForestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip forestSound;

    private static readonly string[] blockedLines = {"<color=#531182>Lucas:</color> Ainda não, ainda há algo que não vi aqui"};

    public void Interact()
    {   
        if (ProgressionManager.Instance.currentDay == 2 && ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Night && !(ProgressionManager.Instance.act5NewspaperFound && ProgressionManager.Instance.act5JournalRecovered))
        {
            StartCoroutine(ThoughtUI.Instance.PlaySequence(blockedLines));
            return;
        }
        StartCoroutine(ExitRoutine());
    }

    private IEnumerator ExitRoutine()
    {
        AudioManager.Instance.PlaySFX(forestSound);

        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.Forest,
            SceneRouteManager.EntryPoint.FromMarket
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);

        yield return NarrationUI.Instance.ShowTextRoutine("", route.SceneName);
    }
}