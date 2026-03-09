using UnityEngine;
using System.Collections;

public class Act1Manager : MonoBehaviour
{
    // AUDIO
    [Header("Audio")]
    public AudioClip apartmentMusic;
    public AudioClip apartmentAmbience;
    public AudioClip glassCrack;

    // UI
    [Header("UI")]
    public DialogueUI blackScreenText;

    // SETTINGS
    [Header("Exit Dialogue Settings")]
    public DialogueSettings doorExitText;

    void Start()
    {
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        // Start ambience
        AudioManager.Instance.PlayMusic(apartmentMusic);
        AudioManager.Instance.PlayAmbient(apartmentAmbience);

        // Intro thought
        string[] intro =
        {
            "<color=#531182>Lucas:</color> O advogado deixou o caderno do vovô na mesa."
        };

        yield return ThoughtUI.Instance.PlaySequence(intro);
    }

    public void ExitApartment()
    {
        StartCoroutine(ExitRoutine());
    }

    IEnumerator ExitRoutine()
    {
        // play crack sound
        AudioManager.Instance.PlaySFX(glassCrack);

        // fade music
        yield return StartCoroutine(FadeMusic(2f));

        // show final text
        yield return blackScreenText.ShowTextRoutine(
            "Por um segundo, antes da escuridão total, você sente que o seu reflexo no espelho continuou parado, observando suas costas.",
            doorExitText,
            "Farm_Day"
        );
    }

    IEnumerator FadeMusic(float duration)
    {
        float startVolume = AudioManager.Instance.musicSource.volume;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            AudioManager.Instance.musicSource.volume =
                Mathf.Lerp(startVolume, 0, timer / duration);

            yield return null;
        }

        AudioManager.Instance.musicSource.Stop();
    }
}