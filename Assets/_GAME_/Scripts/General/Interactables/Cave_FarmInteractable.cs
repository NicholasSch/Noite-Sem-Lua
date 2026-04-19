using System.Collections;
using UnityEngine;

public class Cave_FarmInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        StartCoroutine(ExitRoutine());
    }

    private IEnumerator ExitRoutine()
    {
        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.Farm,
            SceneRouteManager.EntryPoint.FromCave
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);

        yield return NarrationUI.Instance.ShowTextRoutine("", route.SceneName);
    }
}