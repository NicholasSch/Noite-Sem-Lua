using System.Collections;
using UnityEngine;

public class Forest_MarketInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip marketSound;

    public void Interact()
    {
        StartCoroutine(ExitRoutine());
    }

    private IEnumerator ExitRoutine()
    {
        AudioManager.Instance.PlaySFX(marketSound);

        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.Market,
            SceneRouteManager.EntryPoint.FromForest
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);

        yield return NarrationUI.Instance.ShowTextRoutine("", route.SceneName);
    }
}