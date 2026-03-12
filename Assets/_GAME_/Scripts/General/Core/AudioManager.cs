using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambientSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource uiSource;
    [SerializeField] private float defaultFadeDuration = 1.5f;

    private Coroutine musicFadeRoutine;
    private Coroutine ambientFadeRoutine;

    public AudioSource MusicSource => musicSource;
    public AudioSource AmbientSource => ambientSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        PlayMusic(clip, defaultFadeDuration);
    }

    public void PlayMusic(AudioClip clip, float fadeDuration)
    {
        if (clip == null)
            return;

        if (musicFadeRoutine != null)
        {
            StopCoroutine(musicFadeRoutine);
        }

        musicFadeRoutine = StartCoroutine(FadeInSource(musicSource, clip, fadeDuration));
    }

    public void StopMusic()
    {
        StopMusic(defaultFadeDuration);
    }

    public void StopMusic(float fadeDuration)
    {
        if (musicFadeRoutine != null)
        {
            StopCoroutine(musicFadeRoutine);
        }

        musicFadeRoutine = StartCoroutine(FadeOutSource(musicSource, fadeDuration));
    }

    public void PlayAmbient(AudioClip clip)
    {
        PlayAmbient(clip, defaultFadeDuration);
    }

    public void PlayAmbient(AudioClip clip, float fadeDuration)
    {
        if (clip == null)
            return;

        if (ambientFadeRoutine != null)
        {
            StopCoroutine(ambientFadeRoutine);
        }

        ambientFadeRoutine = StartCoroutine(FadeInSource(ambientSource, clip, fadeDuration));
    }

    public void StopAmbient()
    {
        StopAmbient(defaultFadeDuration);
    }

    public void StopAmbient(float fadeDuration)
    {
        if (ambientFadeRoutine != null)
        {
            StopCoroutine(ambientFadeRoutine);
        }

        ambientFadeRoutine = StartCoroutine(FadeOutSource(ambientSource, fadeDuration));
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
            return;

        sfxSource.PlayOneShot(clip);
    }

    public void PlayUI(AudioClip clip)
    {
        if (clip == null)
            return;

        uiSource.PlayOneShot(clip);
    }

    private IEnumerator FadeInSource(AudioSource source, AudioClip clip, float duration)
    {
        float targetVolume = source.volume;

        if (source.isPlaying)
        {
            yield return FadeSourceVolume(source, source.volume, 0f, duration * 0.5f);
            source.Stop();
        }

        source.clip = clip;
        source.volume = 0f;
        source.Play();

        yield return FadeSourceVolume(source, 0f, targetVolume, duration);
    }

    private IEnumerator FadeOutSource(AudioSource source, float duration)
    {
        if (!source.isPlaying)
            yield break;

        float startVolume = source.volume;

        yield return FadeSourceVolume(source, startVolume, 0f, duration);

        source.Stop();
        source.volume = startVolume;
    }

    private IEnumerator FadeSourceVolume(AudioSource source, float startVolume, float endVolume, float duration)
    {
        if (duration <= 0f)
        {
            source.volume = endVolume;
            yield break;
        }

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            source.volume = Mathf.Lerp(startVolume, endVolume, timer / duration);
            yield return null;
        }

        source.volume = endVolume;
    }
}