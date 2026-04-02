using System.Collections;
using UnityEngine;

public class BenchVisionCutsceneController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private FarmDay2Manager farmDay2Manager;
    [SerializeField] private PlayerController player;

    [Header("Audio")]
    [SerializeField] private AudioClip sadMusicClip;
    [SerializeField] private AudioClip farmMusicClip;
    [SerializeField] private AudioClip farmAmbienceClip;
    [SerializeField] private AudioClip liasCough;

    [Header("Cutscene objects")]
    [SerializeField] private GameObject presentSapling;
    [SerializeField] private GameObject whiteTreeObject;
    [SerializeField] private GameObject danteSilhouetteObject;
    [SerializeField] private NPCController danteController;
    [SerializeField] private GameObject liaSilhouetteObject;
    [SerializeField] private Transform visionLookTarget;
    [SerializeField] private GameObject mapGridPresent;
    [SerializeField] private GameObject mapGridPast;

    public IEnumerator PlayVision()
    {
        if (ProgressionManager.Instance.act3BenchVisionSeen)
            yield break;

        GameStateManager.SetState(GameState.Cutscene);

        player.LookAtTarget(visionLookTarget);
        GameUI.Instance.gameObject.SetActive(false);

        yield return AudioManager.Instance.FadeInMusicRoutine(sadMusicClip, 2f);

        mapGridPresent.SetActive(false);
        mapGridPast.SetActive(true);
        presentSapling.SetActive(false);
        whiteTreeObject.SetActive(true);
        danteSilhouetteObject.SetActive(true);
        danteController.FaceDirection(NPCController.Direction.Up);
        liaSilhouetteObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1.5f);

        string[] visionLines =
        {
            "<color=#F10B81>Lia:</color> Dante, olhe como tudo cresceu!",
            "Este Engenho será o lugar mais feliz do mundo para o nosso neto.",
            "<color=#4B4B4B>Dante Jovem:</color> Enquanto eu estiver aqui, Lia, nada de ruim vai tocar este chão.",
            "Eu prometo proteger você e este lugar para sempre."
        };

        yield return ThoughtUI.Instance.PlaySequence(visionLines);

        AudioManager.Instance.PlaySFX(liasCough);

        string[] endingLines =
        {
            "<color=#F10B81>Lia:</color> Cough cough."
        };

        yield return ThoughtUI.Instance.PlaySequence(endingLines);

        whiteTreeObject.SetActive(false);
        danteSilhouetteObject.SetActive(false);
        liaSilhouetteObject.SetActive(false);
        mapGridPresent.SetActive(true);
        mapGridPast.SetActive(false);


        yield return AudioManager.Instance.FadeOutMusicRoutine(3f);
        AudioManager.Instance.PlayAmbient(farmAmbienceClip);
        yield return AudioManager.Instance.FadeInMusicRoutine(farmMusicClip, 3f);

        string[] CutscenePrologueLines =
        {
            "<color=#531182>Lucas:</color> O que foi isso"
        };

        yield return ThoughtUI.Instance.PlaySequence(CutscenePrologueLines);

        GameUI.Instance.gameObject.SetActive(true);

        GameStateManager.SetState(GameState.Gameplay);

        farmDay2Manager.MarkBenchVisionSeen();
    }
}