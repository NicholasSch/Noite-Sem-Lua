using System.Collections;
using UnityEngine;

public class FarmDay4Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dayFarmMusic;
    [SerializeField] private AudioClip dayFarmAmbience;

    [Header("Act 7 Objects")]
    [SerializeField] private GameObject firstDigInteractable;
    [SerializeField] private GameObject CorpoSecoNewspaper;
    [SerializeField] private GameObject millMessageInteractable;
    [SerializeField] private GameObject CorpoSecoEncounterTrigger;
    [SerializeField] private GameObject secondDigInteractable;

    private void Start()
    {
        ApplySavedWorldState();
        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        AudioManager.Instance.PlayAmbient(dayFarmAmbience);
        yield return new WaitForSecondsRealtime(2f);
        AudioManager.Instance.PlayMusic(dayFarmMusic);
    }

    public void ApplySavedWorldState()
    {
        bool firstDigRevealed = ProgressionManager.Instance.act7FirstDigInteracted;
        bool newspaperFound = ProgressionManager.Instance.act7NewspaperFound;
        bool millMessageFound = ProgressionManager.Instance.act7MillMessageFound;
        bool secondDigRevealed = ProgressionManager.Instance.act7SecondDigRevealed;
        bool pocketWatchFound = ProgressionManager.Instance.act7PocketWatchFound;

        firstDigInteractable.SetActive(!firstDigRevealed);
        CorpoSecoNewspaper.SetActive(firstDigRevealed && !newspaperFound);
        millMessageInteractable.SetActive(newspaperFound && !millMessageFound);
        CorpoSecoEncounterTrigger.SetActive(millMessageFound && !secondDigRevealed);
        secondDigInteractable.SetActive(secondDigRevealed && !pocketWatchFound);
    }

    public void RevealFirstDig()
    {
        if (ProgressionManager.Instance.act7FirstDigInteracted)
            return;

        ProgressionManager.Instance.act7FirstDigInteracted = true;
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    public void MarkAct7NewspaperFound()
    {
        if (ProgressionManager.Instance.act7NewspaperFound)
            return;

        ProgressionManager.Instance.act7NewspaperFound = true;
        TaskManager.Instance.CompleteTask("Act7_FirstDig");
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    public void MarkAct7MillMessageFound()
    {
        if (ProgressionManager.Instance.act7MillMessageFound)
            return;

        ProgressionManager.Instance.act7MillMessageFound = true;
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    public void RevealSecondDig()
    {
        if (ProgressionManager.Instance.act7SecondDigRevealed)
            return;

        ProgressionManager.Instance.act7SecondDigRevealed = true;
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    public void MarkPocketWatchFound()
    {
        if (ProgressionManager.Instance.act7PocketWatchFound)
            return;

        ProgressionManager.Instance.act7PocketWatchFound = true;
        TaskManager.Instance.CompleteTask("Act7_SecondDig");
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }
}