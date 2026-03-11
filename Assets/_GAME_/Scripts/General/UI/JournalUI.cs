using TMPro;
using UnityEngine;

public class JournalUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leftPageText;
    [SerializeField] private TextMeshProUGUI rightPageText;

    public void Setup(string leftText, string rightText)
    {
        leftPageText.text = leftText;
        rightPageText.text = rightText;
    }

    public void CloseJournal()
    {
        GameStateManager.SetState(GameState.Gameplay);
        Destroy(gameObject);
    }
}