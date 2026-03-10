using System.Collections;
using UnityEngine;

public class JournalInteractable : MonoBehaviour, IInteractable
{
    public GameObject GameUI;
    public GameObject LetterUI;

    public bool isOpen = false;

    public void Awake()
    {
    GameUI.SetActive(false);
    Time.timeScale = 1f;
    }

public void Interact()
    {
        StartCoroutine(OpenLetterRoutine());
    }

    private IEnumerator OpenLetterRoutine()
    {
        GameStateManager.CurrentState = GameState.Letter;
        LetterUI.SetActive(true);
        Time.timeScale = 0f;
        isOpen = true;
        yield return new WaitForSecondsRealtime(10f);

        Close();
    }

    public void Close()
    {
    LetterUI.SetActive(false);
    GameUI.SetActive(true);
    Time.timeScale = 1f;

    ProgressionManager.Instance.LetterOpened = true;
    GameStateManager.CurrentState = GameState.Gameplay;
    }

    public bool LetterIsOpen()
    {
    return isOpen;
    }
}