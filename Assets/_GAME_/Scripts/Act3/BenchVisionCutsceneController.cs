using System.Collections;
using UnityEngine;

public class BenchVisionCutsceneController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Act3FarmManager act3FarmManager;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private PlayerController player;

    [Header("Audio")]
    [SerializeField] private AudioClip musicBoxClip;
    [SerializeField] private AudioClip farmMusicClip;
    [SerializeField] private AudioClip farmAmbienceClip;
    [SerializeField] private AudioClip liasCough;

    [Header("Cutscene Points")]
    [SerializeField] private GameObject presentSapling;
    [SerializeField] private GameObject whiteTreeObject;
    [SerializeField] private GameObject danteSilhouetteObject;
    [SerializeField] private NPCController danteController;
    [SerializeField] private GameObject liaSilhouetteObject;
    [SerializeField] private Transform visionLookTarget;

    public IEnumerator PlayVision()
    {
        if (ProgressionManager.Instance.act3BenchVisionSeen)
            yield break;

        GameStateManager.SetState(GameState.Cutscene);

        gameUI.gameObject.SetActive(false);


        player.LookAtTarget(visionLookTarget);

        AudioManager.Instance.PlayMusic(musicBoxClip);

        presentSapling.SetActive(false);
        whiteTreeObject.SetActive(true);
        danteSilhouetteObject.SetActive(true);
        danteController.FaceDirection(NPCController.Direction.Up);
        liaSilhouetteObject.SetActive(true);

        yield return new WaitForSecondsRealtime(0.8f);

        string[] visionLines =
        {
            "<color=#F10B81>Lia:</color> Dante, olhe como tudo cresceu!", 
            "Este Engenho será o lugar mais feliz do mundo para o nosso neto.",
            "<color=#4B4B4B>Dante Jovem:</color> Enquanto eu estiver aqui, Lia, nada de ruim vai tocar este chão.",
            "Eu prometo proteger você e este lugar para sempre."
        };

        yield return ThoughtUI.Instance.PlaySequence(visionLines);

        string[] endingLines =
        {
            "Cough cough.",
        };

        AudioManager.Instance.PlaySFX(liasCough);
        yield return ThoughtUI.Instance.PlaySequence(endingLines);

        presentSapling.SetActive(true);
        whiteTreeObject.SetActive(false);
        danteSilhouetteObject.SetActive(false);
        liaSilhouetteObject.SetActive(false);
        

        act3FarmManager.MarkBenchVisionSeen();

        AudioManager.Instance.PlayAmbient(farmAmbienceClip);
        AudioManager.Instance.PlayMusic(farmMusicClip);

        gameUI.gameObject.SetActive(true);

        GameStateManager.SetState(GameState.Gameplay);
    }
}