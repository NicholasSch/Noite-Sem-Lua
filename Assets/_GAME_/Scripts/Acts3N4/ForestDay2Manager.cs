using UnityEngine;

public class ForestDay2Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dayForestAmbience;


    void Start()
    {
        AudioManager.Instance.PlayAmbient(dayForestAmbience);
        GameUI.Instance.gameObject.SetActive(!ProgressionManager.Instance.act4HideGameUI);
    }
}