using System.Collections;
using UnityEngine;

public class Act1Manager : MonoBehaviour
{   
    [Header("Audio")]
    [SerializeField] private AudioClip apartmentMusic;
    [SerializeField] private AudioClip apartmentAmbience;
    [SerializeField] private AudioClip glassCrack;
    
    [Header("UIS")]
    [SerializeField] private NarrationSettings doorExitText;

    private void Start()
    {
        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        AudioManager.Instance.PlayAmbient(apartmentAmbience);
        GameUI.Instance.gameObject.SetActive(false);

        string[] intro =
        {
            "<color=#531182>Lucas:</color> O advogado deixou o caderno do vovô na mesa."
        };

        yield return new WaitForSecondsRealtime(2f);
        AudioManager.Instance.PlayMusic(apartmentMusic,2f);
        yield return new WaitForSecondsRealtime(1.5f);
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

        AudioManager.Instance.StopMusic(2f);
        AudioManager.Instance.StopAmbient(2f);
        AudioManager.Instance.PlaySFX(glassCrack);

        yield return NarrationUI.Instance.ShowTextRoutine(
            "Por um segundo, antes da escuridão total, você sente que o seu reflexo no espelho continuou parado, observando suas costas.",
            doorExitText,
            SceneRouteManager.GetScene(SceneRouteManager.WorldArea.Farm)
        );
    }
}