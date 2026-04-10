using System.Collections;
using UnityEngine;

public class GreenFeatherInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private HouseNight3Manager houseNight3Manager;

    private bool isRunning;

    public void Interact()
    {
        if (isRunning)
            return;
        if (!ProgressionManager.Instance.act6NoteFound)
            return;
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        isRunning = true;
        GameStateManager.SetState(GameState.Thought);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> Uma pena verde...",
            "E pó de mico espalhado pelo chão.",
            "Foi o Saci quem me mostrou isso.",
            "A pista segue para o oeste... para a caverna."
        });

        gameObject.SetActive(false);

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;

        houseNight3Manager.CaveClueInteracted();
    }
}