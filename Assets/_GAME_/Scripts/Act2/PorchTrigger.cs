using UnityEngine;
using System.Collections;

public class PorchTrigger : MonoBehaviour
{

    PlayerController player;

    string[] lines =
    {
        "Lucas: O lugar está em pedaços, mas parece... vivo.",
        "Aquela mulher mencionou a feira.",
        "E o vovô deixou uma lista de mantimentos no final do caderno.",
        "Se eu quiser passar mais do que uma noite aqui, preciso de suprimentos.",
        "Vou descansar e partir para a vila logo cedo."
    };

    string[] incompleteTasks =
    {
        "Lucas: Ainda tem coisas que preciso verificar no engenho."
    };

        void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (!other.CompareTag("Player")) return;

        if (!TaskManager.Instance.IsCompleted("Barn_Tools") ||
            !TaskManager.Instance.IsCompleted("Mill_Gears"))
        {
            return;
        }

        player.ForceFaceDown();
        StartCoroutine(StartAct4());
    }

    IEnumerator StartAct4()
    {
        GameStateManager.CurrentState = GameState.Cutscene;

        yield return ThoughtUI.Instance.PlaySequence(lines);

        GameStateManager.CurrentState = GameState.Gameplay;

        Destroy(gameObject);
    }
}