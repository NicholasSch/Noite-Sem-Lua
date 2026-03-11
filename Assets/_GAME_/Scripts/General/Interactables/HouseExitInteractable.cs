using System.Collections;
using UnityEngine;

public class HouseExitInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private NarrationUI narrationUI;

    public void Interact()
    {
        StartCoroutine(ExitRoutine());
    }

    private IEnumerator ExitRoutine()
    {
        AudioManager.Instance.PlaySFX(doorSound);

        SceneRouteManager.RouteData route = SceneRouteManager.GetRoute(
            SceneRouteManager.WorldArea.Farm,
            SceneRouteManager.EntryPoint.FromHouse
        );

        ProgressionManager.Instance.SetPendingSpawn(route.SceneName, route.SpawnPointID);

        yield return narrationUI.ShowTextRoutine("", route.SceneName);
    }
}