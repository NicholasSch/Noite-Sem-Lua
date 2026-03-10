using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject journalPrefab;

    public Transform scenarioUIParent; 

    JournalUI journal;

    public void OpenJournal()
    {
        if(GameStateManager.CurrentState == GameState.Gameplay)
        {
            GameObject obj = Instantiate(journalPrefab, scenarioUIParent);


            journal = obj.GetComponent<JournalUI>();
            journal.Setup(
                JournalSystem.Instance.GetLeftPage(),
                JournalSystem.Instance.GetRightPage()
            );

            GameStateManager.CurrentState = GameState.Journal;
        }
    }
}
