using System.Collections;
using UnityEngine;

public class Act6ProtectionInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay3Manager farmDay3Manager;
    [SerializeField] private AudioClip fireSound;

    public void Interact()
    {
        StartCoroutine(InteractRoutine());
    }

    private IEnumerator InteractRoutine()
    {
        string[] blockedLines =
        {
            "<color=#531182>Lucas:</color> eu deveria ir ver a árvore primeiro "
        };

        if (!ProgressionManager.Instance.IsTaskCompleted("Sentinel_Thirst"))
        {
            yield return ThoughtUI.Instance.PlaySequence(blockedLines);
            yield break;
        }

        yield return Routine();
    }

    private IEnumerator Routine()
    {
        GameStateManager.SetState(GameState.Thought);

        AudioManager.Instance.PlaySFX(fireSound);

        string[] lines =
        {
            "<color=#531182>Lucas:</color> Arruda... guiné...",
            "O caderno manda deixar isso queimando na fogueira.",
            "\"Para manter o redemoinho longe.\"",
            "Quanto mais eu leio o que o vovô escreveu, menos isso parece superstição.",
            "Se alguma coisa entrar aqui hoje, eu quero a casa pronta.",
            "Tenho me sentido tão cansado ...",
            "Não parece que tenho mais nada a fazer",
            "eu deveria entrar"
        };

        yield return ThoughtUI.Instance.PlaySequence(lines);

        farmDay3Manager.CompleteHouseWhistleTask();
        gameObject.SetActive(false);

        GameStateManager.SetState(GameState.Gameplay);
    }
}