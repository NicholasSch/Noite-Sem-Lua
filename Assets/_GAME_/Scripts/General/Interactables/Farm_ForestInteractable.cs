using System.Collections;
using UnityEngine;

public class Farm_ForestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip forestSound;

    public void Interact()
    {
        StartCoroutine(ExitRoutine());
    }

    private IEnumerator ExitRoutine()
    {
        AudioManager.Instance.PlaySFX(forestSound);

        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.Forest,
            SceneRouteManager.EntryPoint.FromFarm
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);

        yield return NarrationUI.Instance.ShowTextRoutine("", route.SceneName);
    }
}