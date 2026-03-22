using UnityEngine;

public class MarketNight2Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip NightForestAmbience;

    [Header("Dependencies")]
    [SerializeField] private GameUI gameUI;
    [SerializeField] private GameObject journalInteractable;
    [SerializeField] private GameObject newspaperCurupiraInteractable;

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(NightForestAmbience);
        ApplySavedWorldState();
    }

    private void ApplySavedWorldState()
    {
        gameUI.gameObject.SetActive(!ProgressionManager.Instance.act4HideGameUI);
        journalInteractable.SetActive(!ProgressionManager.Instance.act5JournalRecovered);
        newspaperCurupiraInteractable.SetActive(!ProgressionManager.Instance.act5NewspaperFound);
    }

    public void MarkJournalInteracted()
    {
        ProgressionManager.Instance.act5JournalRecovered = true;
        ProgressionManager.Instance.act4HideGameUI = false;
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    public void MarkNewspaperFound()
    {
        ProgressionManager.Instance.act5NewspaperFound = true;
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }
}