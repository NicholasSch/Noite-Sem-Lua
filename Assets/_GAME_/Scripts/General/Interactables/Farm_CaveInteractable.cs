using System.Collections;
using UnityEngine;

public class Farm_CaveInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
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