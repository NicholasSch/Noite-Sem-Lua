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
        bool firstDigRevealed = ProgressionManager.Instance.act7FirstDigRevealed;
        bool newspaperFound = ProgressionManager.Instance.act7NewspaperFound;
        bool millMessageFound = ProgressionManager.Instance.act7MillMessageFound;

        firstDigInteractable.SetActive(!firstDigRevealed);
        CorpoSecoNewspaper.SetActive(firstDigRevealed && !newspaperFound);
        millMessageInteractable.SetActive(newspaperFound && !millMessageFound);
    }

    public void RevealFirstDig()
    {
        if (ProgressionManager.Instance.act7FirstDigRevealed)
            return;

        ProgressionManager.Instance.act7FirstDigRevealed = true;
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
}