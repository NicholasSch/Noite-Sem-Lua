using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class InGameMenuManager : MonoBehaviour
{
    public static bool isPaused = false;

    [Header("UI Panels")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject optionsMenuUI;

    [Header("Audio")]
    [SerializeField] private AudioClip openMenuSound;
    [SerializeField] private AudioClip closeMenuSound;

    private void Start()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (GameStateManager.CurrentState == GameState.Menu) return;

        if (optionsMenuUI.activeSelf)
        {
            CloseOptions();
            return;
        }

        if (isPaused)
        {
            Resume();
        }
        else
        {
            if (GameStateManager.CurrentState == GameState.Gameplay)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        
        Time.timeScale = 1f;
        isPaused = false;
        GameStateManager.SetState(GameState.Gameplay);
        
        AudioManager.Instance.PlayUI(closeMenuSound);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        
        Time.timeScale = 0f;
        isPaused = true;
        GameStateManager.SetState(GameState.Paused);

        AudioManager.Instance.PlayUI(openMenuSound);
    }

    public void OpenJournal()
    {   
        Resume();
        GameUI.Instance.OpenJournal();
        pauseMenuUI.SetActive(false);
    }

    public void OpenOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;

        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        
        GameStateManager.SetState(GameState.Menu);
        
        SceneManager.LoadScene("Menu"); 
    }
}