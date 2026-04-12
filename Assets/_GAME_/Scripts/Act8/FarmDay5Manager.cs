using System.Collections;
using UnityEngine;

public class FarmDay5Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dayFarmMusic;
    [SerializeField] private AudioClip dayFarmAmbience;

    [Header("Act 8 Objects")]
    [SerializeField] private GameObject treeInteractable;
    [SerializeField] private GameObject act8NewspaperInteractable;
    [SerializeField] private GameObject campfireInteractable;
    [SerializeField] private GameObject hairInteractable;

    private void Start()
    {
        ApplySavedWorldState();
        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        AudioManager.Instance.PlayAmbient(dayFarmAmbience);
        yield return new WaitForSecondsRealtime(1.5f);
        AudioManager.Instance.PlayMusic(dayFarmMusic);
    }

    public void ApplySavedWorldState()
    {
        bool pineconeFound = ProgressionManager.Instance.act8PineconeFound;
        bool newspaperFound = ProgressionManager.Instance.act8NewspaperFound;
        bool ritualDone = ProgressionManager.Instance.act8RitualDone;
        bool hairFound = ProgressionManager.Instance.act8HairFound;

        treeInteractable.SetActive(!pineconeFound);
        act8NewspaperInteractable.SetActive(!newspaperFound);
        campfireInteractable.SetActive(pineconeFound && newspaperFound && !ritualDone);
        hairInteractable.SetActive(ritualDone && !hairFound);
    }

    public void MarkPineconeFound()
    {
        if (ProgressionManager.Instance.act8PineconeFound)
            return;

        ProgressionManager.Instance.act8PineconeFound = true;
        TaskManager.Instance.CompleteTask("Act8_TreeHeart");
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    public void MarkAct8NewspaperFound()
    {
        if (ProgressionManager.Instance.act8NewspaperFound)
            return;

        ProgressionManager.Instance.act8NewspaperFound = true;
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    public void MarkRitualDone()
    {
        if (ProgressionManager.Instance.act8RitualDone)
            return;

        ProgressionManager.Instance.act8RitualDone = true;
        TaskManager.Instance.CompleteTask("Act8_FireRitual");
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    public void MarkHairFound()
    {
        if (ProgressionManager.Instance.act8HairFound)
            return;

        ProgressionManager.Instance.act8HairFound = true;
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }
}