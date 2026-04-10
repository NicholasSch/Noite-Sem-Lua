using System.Collections;
using UnityEngine;

public class NewspaperCorpoSecoInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay4Manager farmDay4Manager;
    [SerializeField] private NewspaperUI newspaperPrefab;

    private bool isRunning;

    private static readonly string[] lines =
    {
        "<color=#531182>Lucas:</color> O vovô enterrou isso bem debaixo do moinho...",
        "Se ele queria esconder a verdade aqui,",
        "talvez tenha deixado mais alguma coisa na própria madeira."
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

        farmDay4Manager.MarkAct7NewspaperFound();
    }
}