using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject journalPrefab;

    private JournalUI journal;

    public void OpenJournal()
    {
        if (GameStateManager.CurrentState != GameState.Gameplay)
            return;

        GameObject obj = Instantiate(journalPrefab);

        journal = obj.GetComponent<JournalUI>();
        journal.Setup(
            JournalSystem.Instance.GetLeftPage(),
            JournalSystem.Instance.GetRightPage()
        );

        GameStateManager.SetState(GameState.Journal);
    }
}