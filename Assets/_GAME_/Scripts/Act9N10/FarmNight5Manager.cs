using UnityEngine;

public class FarmNight5Manager : MonoBehaviour
{
    [SerializeField] private AudioClip nightAmbience;
    [SerializeField] private AudioClip nightMusic;
    private void Start()
        {
            AudioManager.Instance.PlayAmbient(nightAmbience);
            AudioManager.Instance.PlayMusic(nightMusic,2f);
        }
}
