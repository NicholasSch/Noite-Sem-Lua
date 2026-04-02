using UnityEngine;

public class FarmNight2Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip NightFarmAmbience;


    private void Start()
    {
        AudioManager.Instance.PlayAmbient(NightFarmAmbience);
        GameUI.Instance.gameObject.SetActive(!ProgressionManager.Instance.act4HideGameUI);
    }
}