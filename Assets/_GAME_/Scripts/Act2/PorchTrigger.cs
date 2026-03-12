using System.Collections;
using UnityEngine;

public class PorchTrigger : MonoBehaviour
{
    private static readonly string[] Lines =
    {
        "Lucas: O lugar está em pedaços, mas parece... vivo.",
        "Aquela mulher mencionou a feira.",
        "E o vovô deixou uma lista de mantimentos no final do caderno.",
        "Se eu quiser passar mais do que uma noite aqui, preciso de suprimentos.",
        "Eu devia descansar e partir para a vila logo cedo."
    };

    private PlayerController player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();

        if (ProgressionManager.Instance.porchScenePlayed)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (!other.CompareTag("Player"))
            return;
        if (ProgressionManager.Instance.porchScenePlayed)
            return;
        if (!TaskManager.Instance.IsCompleted("Barn_Tools") || !TaskManager.Instance.IsCompleted("Mill_Gears") || !ProgressionManager.Instance.HasTalkedToNpc("CucaDisguised"))
            return;
        player.ForceFaceDown();
        StartCoroutine(StartAct4());
    }

    private IEnumerator StartAct4()
    {
        GameStateManager.SetState(GameState.Cutscene);

        yield return ThoughtUI.Instance.PlaySequence(Lines);

        ProgressionManager.Instance.porchScenePlayed = true;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
        Destroy(gameObject);
    }
}