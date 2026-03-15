using System.Collections;
using UnityEngine;

public class Forest_MarketInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip marketSound;
    [SerializeField] private NarrationUI narrationUI;

    public void Interact()
    {
        StartCoroutine(EnterHouseRoutine());
    }

    private IEnumerator EnterHouseRoutine()
    {
        AudioManager.Instance.PlaySFX(marketSound);

        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.Market,
            SceneRouteManager.EntryPoint.FromForest
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);

        yield return narrationUI.ShowTextRoutine("", route.SceneName);
    }
}