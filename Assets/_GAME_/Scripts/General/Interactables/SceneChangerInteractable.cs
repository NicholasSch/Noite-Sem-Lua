using System.Collections;
using UnityEngine;

public class SceneChangerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private SceneRouteManager.WorldArea destination;
    [SerializeField] private SceneRouteManager.EntryPoint destinationEntryPoint;
    [SerializeField] private NarrationUI narrationUI;
    [SerializeField] private AudioClip travelSound;
    [SerializeField] private string transitionText;

    public void Interact()
    {
        StartCoroutine(TravelRoutine());
    }

    private IEnumerator TravelRoutine()
    {
        if (travelSound != null)
        {
            AudioManager.Instance.PlaySFX(travelSound);
        }

        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(destination, destinationEntryPoint);

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);

        yield return narrationUI.ShowTextRoutine(transitionText, route.SceneName);
    }
}