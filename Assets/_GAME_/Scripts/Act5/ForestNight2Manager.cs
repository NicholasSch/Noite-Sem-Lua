using UnityEngine;

public class ForestNight2Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip NightForestAmbience;

    [Header("Dependencies")]
    [SerializeField] private GameUI gameUI;

    [Header("Objects")]
    [SerializeField] private GameObject footstepsInteractable;
    [SerializeField] private GameObject hollowLogInteractable;
    [SerializeField] private GameObject loopTriggerObject;

    private int currentLoopCount;

    public int CurrentLoopCount => currentLoopCount;
    public bool CanUseHollowLog => currentLoopCount > 2 || ProgressionManager.Instance.act5ForestLoopBroken;

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(NightForestAmbience);
        gameUI.gameObject.SetActive(!ProgressionManager.Instance.act4HideGameUI);
        ApplySavedWorldState();
    }

    public void RegisterLoop()
    {
        currentLoopCount++;
        ApplySavedWorldState();
    }

    public void BreakForestLoop()
    {
        ProgressionManager.Instance.act5ForestLoopBroken = true;
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    public void MarkFootstepsSeen()
    {
        ProgressionManager.Instance.act5FootstepsSeen = true;
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    private void ApplySavedWorldState()
    {
        footstepsInteractable.SetActive(!ProgressionManager.Instance.act5FootstepsSeen);
        hollowLogInteractable.SetActive(!ProgressionManager.Instance.act5ForestLoopBroken && currentLoopCount > 2);
        loopTriggerObject.SetActive(!ProgressionManager.Instance.act5ForestLoopBroken);
    }
}