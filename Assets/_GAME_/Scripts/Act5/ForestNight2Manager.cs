using UnityEngine;

public class ForestNight2Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip NightForestAmbience;

    [Header("Dependencies")]
    [SerializeField] private GameUI gameUI;
    [SerializeField] private GameObject footstepsInteractable;

    void Start()
    {
        AudioManager.Instance.PlayAmbient(NightForestAmbience);
        gameUI.gameObject.SetActive(!ProgressionManager.Instance.act4HideGameUI);
        footstepsInteractable.SetActive(!ProgressionManager.Instance.act5JournalRecovered);
    }
}