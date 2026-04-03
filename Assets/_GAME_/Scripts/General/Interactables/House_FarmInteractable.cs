using System.Collections;
using UnityEngine;

public class House_FarmInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip doorSound;
    private static readonly string[] noTobbacoText = {"<color=#531182>Lucas:</color> Eu deveria procurar uma lanterna no depósito antes"};

    private static readonly string[] night3Text = {"<color=#531182>Lucas:</color> Eu deveria esperar ate amanhã"};

    public void Interact()
    {
        if (ProgressionManager.Instance.currentDay == 2 && ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Night && !ProgressionManager.Instance.act5TobaccoFound)
        {
            StartCoroutine(ThoughtUI.Instance.PlaySequence(noTobbacoText));
        }
        else if (ProgressionManager.Instance.currentDay == 3 && ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Night)
        {
            StartCoroutine(ThoughtUI.Instance.PlaySequence(night3Text));
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

        yield return NarrationUI.Instance.ShowTextRoutine("", route.SceneName);
    }
}