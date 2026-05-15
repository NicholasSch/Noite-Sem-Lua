using System.Collections;
using UnityEngine;

public class FarmEpilogueManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private CanvasGroup blackOverlay;
    [SerializeField] private GameObject creditsUI;
    [SerializeField]PlayerController playerController;

    [Header("Audio")]
    [SerializeField] private AudioClip sunnyFarmAmbience;
    [SerializeField] private AudioClip finalMusicTheme;

    private bool finalSequenceTriggered;

    private void Start()
    {
        ProgressionManager.Instance.journalPhase = ProgressionManager.JournalPhase.Epilogue;
        ProgressionManager.Instance.SaveProgress();
        
        blackOverlay.alpha = 0;
        creditsUI.SetActive(false);

        AudioManager.Instance.PlayAmbient(sunnyFarmAmbience);

        GameStateManager.SetState(GameState.Gameplay);
    }

    public void TriggerFinalSequence()
    {
        if (finalSequenceTriggered) return;
        finalSequenceTriggered = true;
        GameUI.Instance.gameObject.SetActive(false);
        StartCoroutine(EpilogueSequence());
    }

    private IEnumerator EpilogueSequence()
    {
        GameStateManager.SetState(GameState.Cutscene);

        playerController.ForceFaceDown();

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "O Engenho de Dante não era feito de pedras ou engrenagens.",
            "Era feito de histórias.",
            "E hoje, uma nova história começa a ser escrita."
        });

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(FadeToBlack(3f));
        
        yield return new WaitForSeconds(2f);

        creditsUI.SetActive(true);

    }

    private IEnumerator FadeToBlack(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            blackOverlay.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        blackOverlay.alpha = 1f;
    }
}