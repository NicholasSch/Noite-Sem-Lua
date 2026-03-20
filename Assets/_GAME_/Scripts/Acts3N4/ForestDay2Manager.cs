using UnityEngine;

public class ForestDay2Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dayForestAmbience;

    [Header("Dependencies")]
    [SerializeField] private GameUI gameUI;

    void Start()
    {
        AudioManager.Instance.PlayAmbient(dayForestAmbience);
        gameUI.gameObject.SetActive(ProgressionManager.Instance.act4HideGameUI);
    }
}
