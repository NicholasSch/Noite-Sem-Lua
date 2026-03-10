using UnityEngine;
using System.Collections;

public class HouseInteractable : MonoBehaviour, IInteractable
{
    public AudioClip doorSound;

    public NarrationUI narrationUI;
    public void Interact()
    {

        StartCoroutine(ExitRoutine());
    }
        IEnumerator ExitRoutine()
        {
        // play door sound
        AudioManager.Instance.PlaySFX(doorSound);

        // fade music
        StartCoroutine(FadeMusic(2f));

        // change scene
        yield return narrationUI.ShowTextRoutine(
            "",
            "House_Day"
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
