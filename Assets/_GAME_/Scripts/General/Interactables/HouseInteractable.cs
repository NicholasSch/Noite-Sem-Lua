using System.Collections;
using UnityEngine;

public class HouseInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private NarrationUI narrationUI;

    public void Interact()
    {
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

        yield return narrationUI.ShowTextRoutine("", route.SceneName);
    }
}