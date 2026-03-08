using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Act1Manager : MonoBehaviour
{
    //Audio
    public AudioClip apartmentMusic;
    public AudioClip apartmentAmbience;
    public AudioClip glassCrack;


    //Data
    public DialogueUI blackScreenText;

    public void Start()
    {
        AudioManager.Instance.PlayMusic(apartmentMusic);
        AudioManager.Instance.PlayAmbient(apartmentAmbience);

            ThoughtUI.Instance.ShowThought(
                "<color=#531182>Lucas:</color> O advogado deixou o caderno do vovô na mesa"
            );
            return;   
    }

    public void ExitApartment()
    {
        StartCoroutine(FadeMusic());
        StartCoroutine(ExitSequence());
    }

private IEnumerator ExitSequence()
{
    AudioManager.Instance.PlaySFX(glassCrack);
    DialogueSettings DoorExitText = new DialogueSettings
    {
        displayDuration = 3f,
        fadeDuration = 1.2f,
        typingSpeed = 0.03f
    };

    yield return StartCoroutine(
        blackScreenText.ShowTextRoutine(
            "Por um segundo, antes da escuridão total, você sente que o seu reflexo no espelho continuou parado, observando suas costas.",
            DoorExitText,"Farm_Day"
        )
    );

}

IEnumerator FadeMusic()
{
    float startVolume = AudioManager.Instance.musicSource.volume;

    while (AudioManager.Instance.musicSource.volume > 0)
    {
        AudioManager.Instance.musicSource.volume -= startVolume * Time.deltaTime;
        yield return null;
    }

    AudioManager.Instance.musicSource.Stop();
}
}