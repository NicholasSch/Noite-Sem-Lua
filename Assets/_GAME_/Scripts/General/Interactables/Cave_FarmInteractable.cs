using System.Collections;
using UnityEngine;

public class Cave_FarmInteractable : MonoBehaviour, IInteractable
{
    private static readonly string[] blockedText =
    {
        "<color=#531182>Lucas:</color> Não... ainda não.",
        "Se eu sair agora, isso nunca vai acabar."
    };

    public void Interact()
    {
        if (ProgressionManager.Instance.act9IntroPlayed && !ProgressionManager.Instance.act9Completed)
        {
            StartCoroutine(ThoughtUI.Instance.PlaySequence(blockedText));
            return;
        }

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