using System.Collections;
using UnityEngine;

public class Act2CurioEncounterController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private FarmDay1Manager farmDay1Manager;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private PlayerController player;

    [Header("Scene Objects")]
    [SerializeField] private GameObject donaCurioObject;
    [SerializeField] private NPCController donaCurioController;
    [SerializeField] private Transform playerLookTarget;
    [SerializeField] private Transform curioExitTarget;

    private bool isRunning;

    public void TriggerEncounter()
    {
        if (isRunning || ProgressionManager.Instance.act2CurioEncounterPlayed)
            return;

        StartCoroutine(PlayEncounter());
    }

    private IEnumerator PlayEncounter()
    {
        isRunning = true;

        GameStateManager.SetState(GameState.Cutscene);
        gameUI.gameObject.SetActive(false);

        player.LookAtTarget(playerLookTarget);
        donaCurioController.LookAtTarget(player.transform);

        string[] lines =
        {
            "<color=#531182>Lucas:</color> Olá? Senhora? Eu sou o Lucas, neto do Dante.",
            "<color=#8CD221>Dona Curió:</color> Cuidado onde pisa, garoto.",
            "A terra do seu avô tem memória curta pras pessoas, mas memória longa pras dívidas.",
            "<color=#531182>Lucas:</color> Dívidas? Meu avô era um homem simples, senhora.",
            "<color=#8CD221>Dona Curió:</color> Neste chão, nada é simples.",
            "Vá até a feira amanhã se quiser ver como o tempo corre diferente aqui.",
            "Mas volte antes do sol se pôr.",
            "O moinho gosta de observar quem anda no escuro."
        };

        string[] Afterlines =
        {
            "<color=#531182>Lucas:</color> Que mulher estranha", 
            "Este lugar é estranho",
            "Bom eu deveria olhar o caderno agora"
        };

        yield return ThoughtUI.Instance.PlaySequence(lines);

        yield return donaCurioController.WalkTo(curioExitTarget.position);

        yield return ThoughtUI.Instance.PlaySequence(Afterlines);

        farmDay1Manager.MarkCurioEncounterPlayed();

        gameUI.gameObject.SetActive(true);
        GameStateManager.SetState(GameState.Gameplay);

        isRunning = false;
    }
}