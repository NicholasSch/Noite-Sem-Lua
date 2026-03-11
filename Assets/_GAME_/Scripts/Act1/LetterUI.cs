using UnityEngine;

public class LetterUI : MonoBehaviour
{
    private JournalInteractable owner;
    private GameObject gameUI;

    public void Setup(JournalInteractable journalInteractable, GameObject uiRoot)
    {
        owner = journalInteractable;
        gameUI = uiRoot;
    }

    public void Close()
    {
        if (gameUI != null)
        {
            gameUI.SetActive(true);
        }

        Time.timeScale = 1f;

        ProgressionManager.Instance.LetterOpened = true;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
        owner?.NotifyClosed();

        Destroy(gameObject);
    }
}