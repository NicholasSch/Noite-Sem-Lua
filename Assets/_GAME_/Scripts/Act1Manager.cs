using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Act1Manager : MonoBehaviour
{
    //public AudioSource sfxSource;
    //public AudioClip glassCrack;
    public DialogueUI blackScreenText;

    public void Start()
    {
            ThoughtUI.Instance.ShowThought(
                "<color=#531182>Lucas:</color> Talvez eu devesse ler o que Dante escreveu primeiro..."
            );
            return;   
    }

    public void ExitApartment()
    {
        StartCoroutine(ExitSequence());
    }

private IEnumerator ExitSequence()
{
    DontDestroyOnLoad(blackScreenText.gameObject);
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
}