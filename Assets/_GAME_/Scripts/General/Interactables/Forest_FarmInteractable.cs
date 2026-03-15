using System.Collections;
using UnityEngine;

public class Forest_FarmInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip farmSound;
    [SerializeField] private NarrationUI narrationUI;

    public void Interact()
    {
        StartCoroutine(EnterHouseRoutine());
    }

    private IEnumerator EnterHouseRoutine()
    {
        AudioManager.Instance.PlaySFX(farmSound);

        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.Farm,
            SceneRouteManager.EntryPoint.FromForest
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);

        yield return narrationUI.ShowTextRoutine("", route.SceneName);
    }
}