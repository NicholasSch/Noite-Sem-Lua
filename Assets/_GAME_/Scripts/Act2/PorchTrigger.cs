using System.Collections;
using UnityEngine;

public class PorchTrigger : MonoBehaviour
{
    private static readonly string[] Lines =
    {
        "<color=#531182>Lucas:</color> O lugar está em pedaços, mas parece... vivo.",
        "Aquela mulher mencionou a feira.",
        "E o vovô deixou uma lista de mantimentos no final do caderno.",
        "Se eu quiser passar mais do que uma noite aqui, preciso de suprimentos.",
        "Eu devia descansar e partir pra vila logo cedo."
    };

    private PlayerController player;
    private bool isRunning;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (isRunning)
            return;

        if (ProgressionManager.Instance.porchScenePlayed)
            return;

        if (!TaskManager.Instance.IsCompleted("Barn_Tools"))
            return;

        if (!TaskManager.Instance.IsCompleted("Mill_Gears"))
            return;

        if (!ProgressionManager.Instance.act2CurioEncounterPlayed)
            return;

        player.ForceFaceDown();
        StartCoroutine(StartPorchScene());
    }

    private IEnumerator StartPorchScene()
    {
        isRunning = true;

        GameStateManager.SetState(GameState.Cutscene);

        yield return ThoughtUI.Instance.PlaySequence(Lines);

        ProgressionManager.Instance.porchScenePlayed = true;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
        gameObject.SetActive(false);

        isRunning = false;
    }
}