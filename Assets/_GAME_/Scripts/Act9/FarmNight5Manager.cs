using UnityEngine;

public class FarmNight5Manager : MonoBehaviour
{
    [SerializeField] private AudioClip nightAmbience;
    private void Start()
        {
            AudioManager.Instance.PlayAmbient(nightAmbience);
        }
}
