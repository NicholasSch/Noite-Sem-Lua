using System.Collections;
using UnityEngine;

public class NewspaperSaciInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay3Manager farmDay3Manager;
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

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;

        farmDay3Manager.MarkAct6NewspaperFound();
    }
}