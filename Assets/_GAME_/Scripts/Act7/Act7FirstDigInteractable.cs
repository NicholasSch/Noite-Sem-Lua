using System.Collections;
using UnityEngine;

public class Act7FirstDigInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay4Manager farmDay4Manager;
    [SerializeField] private AudioClip diggingSound;

    private bool isRunning;
    private PlayerController player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void Interact()
    {
        if (isRunning)
            return;

        if (!ProgressionManager.Instance.act7MorningIntroPlayed)
            return;

        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        isRunning = true;
        GameStateManager.SetState(GameState.Thought);

        player.ForceFaceUp();

        if (!ProgressionManager.Instance.act7FirstDigRevealed)
        {
            AudioManager.Instance.PlaySFX(diggingSound);

            yield return new WaitForSecondsRealtime(1.4f);

            farmDay4Manager.RevealFirstDig();

            yield return ThoughtUI.Instance.PlaySequence(new string[]
            {
                "<color=#531182>Lucas:</color> Estava enterrado fundo.",
                "Alguém queria garantir que esse jornal só fosse lido por quem tivesse disposição de sujar as mãos."
            });

            GameStateManager.SetState(GameState.Gameplay);
            isRunning = false;
            yield break;
        }

        farmDay4Manager.MarkAct7NewspaperFound();

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;
    }
}