using System.Collections;
using UnityEngine;

public class Act7CorpoSecoEncounterController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private FarmDay4Manager farmDay4Manager;
    [SerializeField] private PlayerController player;

    [Header("Scene objects")]
    [SerializeField] private GameObject corpoSecoObject;
    [SerializeField] private NPCController corpoSecoController;
    [SerializeField] private Transform playerLookTarget;
    [SerializeField] private Transform corpoSecoPointTarget;
    [SerializeField] private Transform playerLookAfterPointTarget;

    [Header("Audio")]
    [SerializeField] private AudioClip windBurstClip;
    [SerializeField] private string corpoSecoPointAnimation = "Anim_CorpoSeco_Point";

    private bool isRunning;

    private void Start()
    {
        corpoSecoObject.SetActive(false);
    }

    public void TriggerEncounter()
    {
        if (isRunning || ProgressionManager.Instance.act7SecondDigRevealed)
            return;

        StartCoroutine(PlayEncounter());
    }

    private IEnumerator PlayEncounter()
    {
        isRunning = true;

        GameStateManager.SetState(GameState.Cutscene);
        GameUI.Instance.gameObject.SetActive(false);

        player.LookAtTarget(playerLookTarget);

        AudioManager.Instance.PlaySFX(windBurstClip);

        corpoSecoObject.SetActive(true);
        corpoSecoController.PlayIdle();
        corpoSecoController.LookAtTarget(player.transform);

        yield return new WaitForSecondsRealtime(0.8f);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> ...Você de novo.",
            "Ele não está avançando.",
            "Só está...",
            "me mostrando alguma coisa."
        });

        corpoSecoController.LookAtTarget(corpoSecoPointTarget);

        yield return new WaitForSecondsRealtime(0.8f);

        corpoSecoController.PlayAnimationState(corpoSecoPointAnimation);

        yield return new WaitForSecondsRealtime(2f);

        corpoSecoController.ResetToIdle();

        yield return new WaitForSecondsRealtime(0.2f);

       player.LookAtTarget(playerLookAfterPointTarget);

        farmDay4Manager.RevealSecondDig();

        AudioManager.Instance.PlaySFX(windBurstClip);
        corpoSecoObject.SetActive(false);

        GameUI.Instance.gameObject.SetActive(true);
        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;
    }
}