using System.Collections;
using UnityEngine;

public class NewspaperCurupiraInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private MarketNight2Manager marketNight2Manager;
    [SerializeField] private NewspaperUI newspaperPrefab;

    private bool isRunning;

    private static readonly string[] lines =
    {
        "<color=#531182>Lucas:</color> Então é isso...",
        "O fumo...",
        "E a névoa ao norte...",
        "Nada aqui foi deixado por acaso."
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

        marketNight2Manager.MarkNewspaperFound();

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;
    }
}