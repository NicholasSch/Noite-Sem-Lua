using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject journalPrefab;

    private JournalUI journal;

    public static GameUI Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
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