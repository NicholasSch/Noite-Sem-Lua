using UnityEngine;
using TMPro;

public class JournalUI : MonoBehaviour
{
    public TextMeshProUGUI leftPageText;
    public TextMeshProUGUI rightPageText;

    public void Setup(string leftText, string rightText)
    {
        leftPageText.text = leftText;
        rightPageText.text = rightText;
    }

    public void CloseJournal()
    {
        GameStateManager.CurrentState = GameState.Gameplay;
        Destroy(gameObject);
    }
}