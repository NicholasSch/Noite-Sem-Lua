using System.Collections;
using UnityEngine;

public class FarmDay3Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dayFarmMusic;
    [SerializeField] private AudioClip dayFarmAmbience;


    [Header("Act 6 Objects")]
    [SerializeField] private GameObject newspaperInteractable;
    [SerializeField] private GameObject treeInteractable;
    [SerializeField] private GameObject campfireInteractable;

    private void Start()
    {
        ApplySavedWorldState();
        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        AudioManager.Instance.PlayAmbient(dayFarmAmbience);
        yield return new WaitForSecondsRealtime(3f);
        AudioManager.Instance.PlayMusic(dayFarmMusic);
    }

    public void ApplySavedWorldState()
    {
        newspaperInteractable.SetActive(!ProgressionManager.Instance.act6NewspaperFound);
        treeInteractable.SetActive(!TaskManager.Instance.IsCompleted("Sentinel_Thirst"));
        campfireInteractable.SetActive(!TaskManager.Instance.IsCompleted("House_Whistle"));
    }

    public void MarkAct6NewspaperFound()
    {
        if (ProgressionManager.Instance.act6NewspaperFound)
            return;

        ProgressionManager.Instance.act6NewspaperFound = true;
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    public void CompleteSentinelTask()
    {
        if (TaskManager.Instance.IsCompleted("Sentinel_Thirst"))
            return;

        TaskManager.Instance.CompleteTask("Sentinel_Thirst");
        ApplySavedWorldState();
    }

    public void CompleteHouseWhistleTask()
    {
        if (TaskManager.Instance.IsCompleted("House_Whistle"))
            return;

        TaskManager.Instance.CompleteTask("House_Whistle");
        ApplySavedWorldState();
    }
}