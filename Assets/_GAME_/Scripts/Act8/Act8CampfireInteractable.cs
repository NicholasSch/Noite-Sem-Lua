using System.Collections;
using UnityEngine;

public class Act8CampfireInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay5Manager farmDay5Manager;
    [SerializeField] private AudioClip throwInFireSound;

    private bool isRunning;

    public void Interact()
    {
        if (isRunning)
            return;

        if (ProgressionManager.Instance.act8RitualDone)
            return;

        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        isRunning = true;

        if (!ProgressionManager.Instance.act8PineconeFound)
        {
            GameStateManager.SetState(GameState.Thought);

            yield return ThoughtUI.Instance.PlaySequence(new string[]
            {
                "<color=#531182>Lucas:</color> Ainda falta alguma coisa.",
            });

            GameStateManager.SetState(GameState.Gameplay);
            isRunning = false;
            yield break;
        }

        GameStateManager.SetState(GameState.Thought);

        AudioManager.Instance.PlaySFX(throwInFireSound);

        yield return new WaitForSecondsRealtime(1f);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> O cheiro...",
            "Não é de fumaça.",
            "Tem cheiro de chuva... de terra molhada...",
            "de café fresco na cozinha da vovó.",
            "Meus pulmões não ardem mais com o enxofre que vem do norte.",
            "Eu consigo ver através da névoa agora.",
            "O caminho está aberto.",
            "o que é isso",
            "Há algo enterrado embaixo da fogueira?"
        });

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;

        farmDay5Manager.MarkRitualDone();
    }
}