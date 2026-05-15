using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject warningPanelNewGame;
    [SerializeField] private GameObject warningPanelDemo;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Button continueButton;

    [Header("Audio")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip menuAmbient;
    private readonly string firstSceneName = "Apartment";

    private string SavePath => Path.Combine(Application.persistentDataPath, "progression.json");

    private void Start()
    {   
        GameStateManager.SetState(GameState.Menu);
        GameUI.Instance.gameObject.SetActive(false);
        if (!File.Exists(SavePath))
        {
            continueButton.interactable = false;
            continueButton.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
        optionsPanel.SetActive(false);
        warningPanelNewGame.SetActive(false);
        warningPanelDemo.SetActive(false);
        AudioManager.Instance.PlayMusic(menuMusic,5f);
        AudioManager.Instance.PlayAmbient(menuAmbient);
    }

    public void ContinueGame()
    {   
        ProgressionManager.Instance.LoadProgress();

        GameStateManager.SetState(GameState.Gameplay);
        
        string targetScene = ProgressionManager.Instance.pendingSceneName;

        if (ProgressionManager.Instance.currentDay != 0)
        {

            if (string.IsNullOrEmpty(targetScene))
            {
                var route = SceneRouteManager.GetRoute(
                    SceneRouteManager.WorldArea.Farm, 
                    SceneRouteManager.EntryPoint.Default
                );
                targetScene = route.SceneName;
            }   
        }

        else
        {
           targetScene = "Apartment"; 
        }

        AudioManager.Instance.StopMusic();
        GameUI.Instance.gameObject.SetActive(true);
        SceneManager.LoadScene(targetScene);
    }

    public void NewGameTrigger()
    {
        if (File.Exists(SavePath))
        {
            warningPanelNewGame.SetActive(true);
        }
        else
        {
            ConfirmNewGame(false);
        }
        
    }

    public void ConfirmNewGame(bool demo)
    {
        ProgressionManager.Instance.ResetProgress();
        AudioManager.Instance.StopMusic();

        GameStateManager.SetState(GameState.Gameplay);

        if (!demo)
        {
            SceneManager.LoadScene(firstSceneName);
        }
        else
        {
            LoadDemo();
        }
    }

        public void DemoTrigger()
    {
        if (File.Exists(SavePath))
        {
            warningPanelDemo.SetActive(true);
        }
        else
        {
            ConfirmNewGame(true);
        }
        
    }

    public void CancelNewGame()
    {
        warningPanelNewGame.SetActive(false);
        warningPanelDemo.SetActive(false);
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void LoadDemo()
    {
    ProgressionManager.Instance.isDemo = true;
        
    ProgressionManager.Instance.currentDay = 2;
    ProgressionManager.Instance.currentPeriod = ProgressionManager.DayPeriod.Day;
    ProgressionManager.Instance.SetJournalPhase(ProgressionManager.JournalPhase.Day2Act3);

    ProgressionManager.Instance.LetterOpened = true;
    ProgressionManager.Instance.farmIntroPlayed = true;
    ProgressionManager.Instance.act2CurioEncounterPlayed = true;
    ProgressionManager.Instance.firstNightSleepDone = true;
    ProgressionManager.Instance.firstNightWakeScenePlayed = true;
    ProgressionManager.Instance.firstNightTitlePlayed = true;
    ProgressionManager.Instance.act3BenchVisionSeen = true;

    ProgressionManager.Instance.SaveProgress();
    GameUI.Instance.gameObject.SetActive(true);
    
    var route = SceneRouteManager.GetRoute(SceneRouteManager.WorldArea.House, SceneRouteManager.EntryPoint.Default);
    SceneManager.LoadScene(route.SceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}