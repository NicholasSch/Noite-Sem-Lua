using System.Collections;
using UnityEngine;

public class Act1Manager : MonoBehaviour
{
    [SerializeField] private AudioClip apartmentMusic;
    [SerializeField] private AudioClip apartmentAmbience;
    [SerializeField] private AudioClip glassCrack;
    [SerializeField] private NarrationUI blackScreenText;
    [SerializeField] private NarrationSettings doorExitText;

    private void Start()
    {
        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        AudioManager.Instance.PlayAmbient(apartmentAmbience);

        string[] intro =
        {
            "<color=#531182>Lucas:</color> O advogado deixou o caderno do vovô na mesa."
        };

        yield return new WaitForSecondsRealtime(3f);
        AudioManager.Instance.PlayMusic(apartmentMusic);
        yield return new WaitForSecondsRealtime(1f);
        yield return ThoughtUI.Instance.PlaySequence(intro);
    }

    public void ExitApartment()
    {
        StartCoroutine(ExitRoutine());
    }

    private IEnumerator ExitRoutine()
    {
        ProgressionManager.Instance.SetDay(1);
        ProgressionManager.Instance.SetPeriod(ProgressionManager.DayPeriod.Day);

        AudioManager.Instance.PlaySFX(glassCrack);
        StartCoroutine(FadeMusic(2f));

        yield return blackScreenText.ShowTextRoutine(
            "Por um segundo, antes da escuridão total, você sente que o seu reflexo no espelho continuou parado, observando suas costas.",
            doorExitText,
            SceneRouteManager.GetScene(SceneRouteManager.WorldArea.Farm)
        );
    }

    private IEnumerator FadeMusic(float duration)
    {
        float startVolume = AudioManager.Instance.MusicSource.volume;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            AudioManager.Instance.MusicSource.volume = Mathf.Lerp(startVolume, 0f, timer / duration);
            yield return null;
        }

        AudioManager.Instance.MusicSource.Stop();
    }
}