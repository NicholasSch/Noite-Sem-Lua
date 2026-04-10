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

    [Header("Audio")]
    [SerializeField] private AudioClip windBurstClip;

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

        AudioManager.Instance.PlaySFX(windBurstClip);

        corpoSecoObject.SetActive(true);
        corpoSecoController.PlayIdle();

        yield return new WaitForSecondsRealtime(0.8f);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> ...Você de novo.",
            "Ele não está avançando.",
            "Só está...",
            "me mostrando alguma coisa.",
            "Ha algo embaixo dele?"
        });

        yield return new WaitForSecondsRealtime(0.6f);

       AudioManager.Instance.PlaySFX(windBurstClip);
       corpoSecoObject.SetActive(false);

       GameUI.Instance.gameObject.SetActive(true);
       GameStateManager.SetState(GameState.Gameplay);
       isRunning = false;

       farmDay4Manager.RevealSecondDig();
    }
}