using System.Collections;
using UnityEngine;

public class NewspaperCorpoSecoInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay4Manager farmDay4Manager;
    [SerializeField] private NewspaperUI newspaperPrefab;

    private bool isRunning;

    private static readonly string[] lines =
    {
    };

    public void Interact()
    {
        if (isRunning)
            return;

        if (FindFirstObjectByType<NewspaperUI>() != null)
            return;

        isRunning = true;

        GameStateManager.SetState(GameState.Cutscene);

        NewspaperUI newspaperInstance = Instantiate(newspaperPrefab);
        newspaperInstance.Setup(OnNewspaperClosed);
    }

    private void OnNewspaperClosed()
    {
        StartCoroutine(OnNewspaperClosedRoutine());
    }

    private IEnumerator OnNewspaperClosedRoutine()
    {
        GameStateManager.SetState(GameState.Thought);

        yield return ThoughtUI.Instance.PlaySequence(lines);

        farmDay4Manager.MarkAct7NewspaperFound();

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;
    }
}