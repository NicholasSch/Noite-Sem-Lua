using System.Collections;
using UnityEngine;

public class Farm_ForestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip forestSound;
    [SerializeField] private NarrationUI narrationUI;

    public void Interact()
    {
        StartCoroutine(EnterHouseRoutine());
    }

    private IEnumerator EnterHouseRoutine()
    {
        AudioManager.Instance.PlaySFX(forestSound);

        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.Forest,
            SceneRouteManager.EntryPoint.FromFarm
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);

        yield return narrationUI.ShowTextRoutine("", route.SceneName);
    }
}