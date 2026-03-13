using System.Collections;
using UnityEngine;

public class JournalInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private LetterUI letterPrefab;

    private LetterUI currentLetterInstance;
    private bool isOpen;

    private void Awake()
    {
        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }

        isOpen = false;
        Time.timeScale = 1f;
    }

    public void Interact()
    {
        if (isOpen || currentLetterInstance != null)
            return;

        StartCoroutine(OpenLetterRoutine());
    }

    private IEnumerator OpenLetterRoutine()
    {
        GameStateManager.SetState(GameState.Letter);

        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }

        currentLetterInstance = Instantiate(letterPrefab);
        currentLetterInstance.Setup(this, gameUI);

        isOpen = true;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(30f);

        if (currentLetterInstance != null)
        {
            currentLetterInstance.Close();
        }
    }

    public bool LetterIsOpen()
    {
        return isOpen;
    }

    public void NotifyClosed()
    {
        currentLetterInstance = null;
        isOpen = false;
    }
}