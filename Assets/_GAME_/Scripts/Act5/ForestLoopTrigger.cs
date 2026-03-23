using System.Collections;
using UnityEngine;

public class ForestLoopTrigger : MonoBehaviour
{  
    [Header("Dependencies")]
    [SerializeField] private ForestNight2Manager forestNight2Manager;
    [SerializeField] private Transform returnPoint;

    private bool isRunning;

    private static readonly string[] FirstLoopLines =
    {
        "<color=#531182>Lucas:</color> ...",
        "Eu já passei por aqui."
    };

    private static readonly string[] SecondLoopLines =
    {
        "<color=#531182>Lucas:</color> Não... de novo não.",
        "Essa trilha está me trazendo de volta pro mesmo lugar."
    };

    private static readonly string[] ThirdLoopLines =
    {
        "<color=#531182>Lucas:</color> Tem alguma coisa errada com essa mata.",
        "Se eu continuar andando assim, vou ficar preso aqui."
    };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (ProgressionManager.Instance.act5ForestLoopBroken)
            return;

        if (isRunning)
            return;

        StartCoroutine(LoopRoutine(other.transform));
    }

    private IEnumerator LoopRoutine(Transform playerTransform)
    {
        isRunning = true;

        GameStateManager.SetState(GameState.Cutscene);

        playerTransform.position = returnPoint.position;

        forestNight2Manager.RegisterLoop();

        if (forestNight2Manager.CurrentLoopCount == 1)
        {
            yield return ThoughtUI.Instance.PlaySequence(FirstLoopLines);
        }
        else if (forestNight2Manager.CurrentLoopCount == 2)
        {
            yield return ThoughtUI.Instance.PlaySequence(SecondLoopLines);
        }
        else
        {
            yield return ThoughtUI.Instance.PlaySequence(ThirdLoopLines);
        }

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;
    }
}