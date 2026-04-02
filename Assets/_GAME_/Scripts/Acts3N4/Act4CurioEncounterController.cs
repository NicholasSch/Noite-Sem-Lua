using System.Collections;
using UnityEngine;

public class Act4CurioEncounterController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private FarmDay2Manager farmDay2Manager;
    [SerializeField] private PlayerController player;

    [Header("Scene objects")]
    [SerializeField] private GameObject donaCurioObject;
    [SerializeField] private NPCController donaCurioController;
    [SerializeField] private Transform playerLookTarget;
    [SerializeField] private Transform curioExitTarget;

    [Header("Audio")]
    [SerializeField] private AudioClip tensionMusic;

    private bool isRunning;

    public void TriggerEncounter()
    {
        if (isRunning || ProgressionManager.Instance.act4CurioEncounterPlayed)
            return;

        StartCoroutine(PlayEncounter());
    }

    private IEnumerator PlayEncounter()
    {
        isRunning = true;

        GameStateManager.SetState(GameState.Cutscene);
        GameUI.Instance.gameObject.SetActive(false);

        player.LookAtTarget(playerLookTarget);
        AudioManager.Instance.PlayMusic(tensionMusic);

        donaCurioObject.SetActive(true);
        donaCurioController.LookAtTarget(player.transform);

        string[] lines =
        {
            "<color=#8CD221>Dona Curió:</color> A feira é um lugar barulhento, não é?",
            "As pessoas falam muito porque têm medo do silêncio.",
            "Mas o silêncio daqui é honesto, Lucas. Ele não mente como os feirantes.",
            "<color=#531182>Lucas:</color> Eles dizem que a senhora não existe, Dona Curió.",
            "<color=#8CD221>Dona Curió:</color> Para eles, eu não existo.",
            "Mas para este Engenho, eu sou a única coisa que sobrou de real."
        };

        yield return ThoughtUI.Instance.PlaySequence(lines);

        yield return donaCurioController.WalkTo(curioExitTarget.position);

        string[] endingLines =
        {
            "<color=#531182>Lucas:</color> Está escurecendo rápido demais...",
            "Melhor eu entrar. Posso organizar o que trouxe lá dentro."
        };

        yield return ThoughtUI.Instance.PlaySequence(endingLines);

        farmDay2Manager.MarkCurioEncounterPlayed();

        isRunning = false;

        GameStateManager.SetState(GameState.Gameplay);
    }
}