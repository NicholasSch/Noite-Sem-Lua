using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambientSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource uiSource;

    [Header("Settings")]
    [SerializeField] private float defaultFadeDuration = 1.5f;

    private Coroutine musicFadeRoutine;
    private Coroutine ambientFadeRoutine;

    public AudioSource MusicSource => musicSource;
    public AudioSource AmbientSource => ambientSource;
    public AudioSource SfxSource => sfxSource;
    public AudioSource UISource => uiSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadVolumes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadVolumes()
    {
        musicSource.volume = PlayerPrefs.GetFloat("MusicVol", 0.4f);
        ambientSource.volume = PlayerPrefs.GetFloat("AmbientVol", 0.45f);
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVol", 0.9f);
        uiSource.volume = PlayerPrefs.GetFloat("UIVol", 0.45f);
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = -1f)
    {
        if (clip == null || musicSource.clip == clip) return;
        float duration = fadeDuration < 0 ? defaultFadeDuration : fadeDuration;

        if (musicFadeRoutine != null) StopCoroutine(musicFadeRoutine);
        musicFadeRoutine = StartCoroutine(FadeInSourceRoutine(musicSource, clip, duration, "MusicVol", 0.4f));
    }

    public void StopMusic(float fadeDuration = -1f)
    {
        float duration = fadeDuration < 0 ? defaultFadeDuration : fadeDuration;
        if (musicFadeRoutine != null) StopCoroutine(musicFadeRoutine);
        musicFadeRoutine = StartCoroutine(FadeOutSourceRoutine(musicSource, duration));
    }

    public void PlayAmbient(AudioClip clip, float fadeDuration = -1f)
    {
        if (clip == null || ambientSource.clip == clip) return;
        float duration = fadeDuration < 0 ? defaultFadeDuration : fadeDuration;

        if (ambientFadeRoutine != null) StopCoroutine(ambientFadeRoutine);
        ambientFadeRoutine = StartCoroutine(FadeInSourceRoutine(ambientSource, clip, duration, "AmbientVol", 0.45f));
    }

    public void StopAmbient(float fadeDuration = -1f)
    {
        float duration = fadeDuration < 0 ? defaultFadeDuration : fadeDuration;
        if (ambientFadeRoutine != null) StopCoroutine(ambientFadeRoutine);
        ambientFadeRoutine = StartCoroutine(FadeOutSourceRoutine(ambientSource, duration));
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null) sfxSource.PlayOneShot(clip);
    }

    public void PlayUI(AudioClip clip)
    {
        if (clip != null) uiSource.PlayOneShot(clip);
    }

    private IEnumerator FadeInSourceRoutine(AudioSource source, AudioClip clip, float duration, string prefKey, float defaultVol)
    {
        float targetVolume = PlayerPrefs.GetFloat(prefKey, defaultVol);

        if (source.isPlaying)
        {
            yield return StartCoroutine(FadeSourceVolume(source, source.volume, 0f, duration * 0.5f));
            source.Stop();
        }

        source.clip = clip;
        source.volume = 0f;
        source.Play();

        yield return StartCoroutine(FadeSourceVolume(source, 0f, targetVolume, duration));
    }

    private IEnumerator FadeOutSourceRoutine(AudioSource source, float duration)
    {
        if (!source.isPlaying) yield break;
        yield return StartCoroutine(FadeSourceVolume(source, source.volume, 0f, duration));
        source.Stop();
    }

    private IEnumerator FadeSourceVolume(AudioSource source, float start, float end, float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        source.volume = end;
    }
}