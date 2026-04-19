using System.Collections;
using UnityEngine;

public class Farm_CaveInteractable : MonoBehaviour, IInteractable
{
    private static readonly string[] blockedLines =
    {
        "<color=#531182>Lucas:</color> Ainda não.",
        "Não estou pronto para entrar aí."
    };

    public void Interact()
    {
        if (!(ProgressionManager.Instance.currentDay == 5 &&
              ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Night))
        {
            StartCoroutine(ThoughtUI.Instance.PlaySequence(blockedLines));
            return;
        }

        StartCoroutine(ExitRoutine());
    }

    private IEnumerator ExitRoutine()
    {
        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.Cave,
            SceneRouteManager.EntryPoint.FromFarm
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);

        yield return NarrationUI.Instance.ShowTextRoutine("", route.SceneName);
    }
}