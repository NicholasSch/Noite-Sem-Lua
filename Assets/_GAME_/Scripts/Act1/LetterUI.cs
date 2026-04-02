using UnityEngine;

public class LetterUI : MonoBehaviour
{
    private JournalInteractable owner;


    public void Setup(JournalInteractable journalInteractable)
    {
        owner = journalInteractable;
    }

    public void Close()
    {
        Time.timeScale = 1f;

        ProgressionManager.Instance.LetterOpened = true;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
        owner?.NotifyClosed();

        Destroy(gameObject);
    }
}