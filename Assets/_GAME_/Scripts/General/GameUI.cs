using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject journalPrefab;

    JournalUI currentJournal;

    public void OpenJournal()
    {   
        if (GameStateManager.CurrentState != GameState.Gameplay)
        {
            return;
        }

        GameStateManager.CurrentState = GameState.Journal;

        GameObject obj = Instantiate(journalPrefab, transform);
        currentJournal = obj.GetComponent<JournalUI>();

        currentJournal.Setup(
            JournalSystem.Instance.GetLeftPage(),
            JournalSystem.Instance.GetRightPage()
        );
    }
}