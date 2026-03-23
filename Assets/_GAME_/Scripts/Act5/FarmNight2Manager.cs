using UnityEngine;

public class FarmNight2Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip NightFarmAmbience;

    [Header("Dependencies")]
    [SerializeField] private GameUI gameUI;

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(NightFarmAmbience);
        gameUI.gameObject.SetActive(!ProgressionManager.Instance.act4HideGameUI);
    }
}