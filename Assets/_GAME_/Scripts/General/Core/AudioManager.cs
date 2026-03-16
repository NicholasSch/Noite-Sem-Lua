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

        musicFadeRoutine = StartCoroutine(FadeInSourceRoutine(musicSource, clip, fadeDuration));
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

        musicFadeRoutine = StartCoroutine(FadeOutSourceRoutine(musicSource, fadeDuration));
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

        ambientFadeRoutine = StartCoroutine(FadeInSourceRoutine(ambientSource, clip, fadeDuration));
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

        ambientFadeRoutine = StartCoroutine(FadeOutSourceRoutine(ambientSource, fadeDuration));
    }

    public IEnumerator FadeInMusicRoutine(AudioClip clip, float duration)
    {
        if (clip == null)
            yield break;

        if (musicFadeRoutine != null)
        {
            StopCoroutine(musicFadeRoutine);
            musicFadeRoutine = null;
        }

        yield return FadeInSourceRoutine(musicSource, clip, duration);
    }

    public IEnumerator FadeOutMusicRoutine(float duration)
    {
        if (musicFadeRoutine != null)
        {
            StopCoroutine(musicFadeRoutine);
            musicFadeRoutine = null;
        }

        yield return FadeOutSourceRoutine(musicSource, duration);
    }

    public IEnumerator FadeInAmbientRoutine(AudioClip clip, float duration)
    {
        if (clip == null)
            yield break;

        if (ambientFadeRoutine != null)
        {
            StopCoroutine(ambientFadeRoutine);
            ambientFadeRoutine = null;
        }

        yield return FadeInSourceRoutine(ambientSource, clip, duration);
    }

    public IEnumerator FadeOutAmbientRoutine(float duration)
    {
        if (ambientFadeRoutine != null)
        {
            StopCoroutine(ambientFadeRoutine);
            ambientFadeRoutine = null;
        }

        yield return FadeOutSourceRoutine(ambientSource, duration);
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

    private IEnumerator FadeInSourceRoutine(AudioSource source, AudioClip clip, float duration)
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

    private IEnumerator FadeOutSourceRoutine(AudioSource source, float duration)
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