using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class OptionsMenuManager : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider ambientSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider uiSlider;

    [Header("Value Labels")]
    [SerializeField] private TextMeshProUGUI musicValueText;
    [SerializeField] private TextMeshProUGUI ambientValueText;
    [SerializeField] private TextMeshProUGUI sfxValueText;
    [SerializeField] private TextMeshProUGUI uiValueText;

    private bool isLoading = false; 

    private void OnEnable()
    {
        LoadSettings();
    }

    public void SetMusicVolume(float value)
    {
        UpdateText(musicValueText, value);
        if (isLoading) return;
        
        AudioManager.Instance.MusicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVol", value);
    }

    public void SetAmbientVolume(float value)
    {
        UpdateText(ambientValueText, value);
        if (isLoading) return;

        AudioManager.Instance.AmbientSource.volume = value;
        PlayerPrefs.SetFloat("AmbientVol", value);
    }

    public void SetSFXVolume(float value)
    {
        UpdateText(sfxValueText, value);
        if (isLoading) return;

        AudioManager.Instance.SfxSource.volume = value; 
        PlayerPrefs.SetFloat("SFXVol", value);
    }

    public void SetUIVolume(float value)
    {   
        UpdateText(uiValueText, value);
        if (isLoading) return;

        AudioManager.Instance.UISource.volume = value; 
        PlayerPrefs.SetFloat("UIVol", value);
    }

    private void LoadSettings()
    {
        isLoading = true; 

        float mVol = PlayerPrefs.GetFloat("MusicVol", 0.4f);
        float aVol = PlayerPrefs.GetFloat("AmbientVol", 0.45f);
        float sVol = PlayerPrefs.GetFloat("SFXVol", 0.9f);
        float uVol = PlayerPrefs.GetFloat("UIVol", 0.45f);

        musicSlider.value = mVol;
        ambientSlider.value = aVol;
        sfxSlider.value = sVol;
        uiSlider.value = uVol;

        UpdateText(musicValueText, mVol);
        UpdateText(ambientValueText, aVol);
        UpdateText(sfxValueText, sVol);
        UpdateText(uiValueText, uVol);

        AudioManager.Instance.MusicSource.volume = mVol;
        AudioManager.Instance.AmbientSource.volume = aVol;
        AudioManager.Instance.SfxSource.volume = sVol;
        AudioManager.Instance.UISource.volume = uVol;

        isLoading = false; 
    }

    private void UpdateText(TextMeshProUGUI textElement, float value)
    {
        if (textElement != null)
        {
            int percentage = Mathf.RoundToInt(value * 100);
            textElement.text = percentage.ToString() + "%";
        }
    }

    public void CloseOptions()
    {
        gameObject.SetActive(false);
    }
}