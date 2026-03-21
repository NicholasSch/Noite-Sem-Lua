using System.Collections;
using UnityEngine;

public class House_FarmInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private NarrationUI narrationUI;
    private static readonly string[] noTobbacoText = {"<color=#531182>Lucas:</color> Eu deveria procurar uma lanterna no depósito antes"};

    public void Interact()
    {
        if (ProgressionManager.Instance.currentDay == 2 && ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Night && !ProgressionManager.Instance.act5TobaccoFound)
        {
            StartCoroutine(ThoughtUI.Instance.PlaySequence(noTobbacoText));
        }
        else
        {
        StartCoroutine(ExitRoutine());
        }
    }

    private IEnumerator ExitRoutine()
    {
        AudioManager.Instance.PlaySFX(doorSound);

        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.Farm,
            SceneRouteManager.EntryPoint.FromHouse
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);

        yield return narrationUI.ShowTextRoutine("", route.SceneName);
    }
}