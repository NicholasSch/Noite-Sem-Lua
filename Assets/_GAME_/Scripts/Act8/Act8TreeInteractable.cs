using System.Collections;
using UnityEngine;

public class Act8TreeInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay5Manager farmDay5Manager;
    [SerializeField] private PlayerController player;
    [SerializeField] private AudioClip heartbeatThump;


    private bool isRunning;

    public void Interact()
    {
        if (isRunning)
            return;

        if (ProgressionManager.Instance.act8PineconeFound)
            return;

        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        isRunning = true;
        GameStateManager.SetState(GameState.Thought);

        player.ForceFaceUp();

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> Uma pinha comum...",
            "Mas parece mais pesada do que deveria."
        });

        AudioManager.Instance.PlaySFX(heartbeatThump);

        yield return new WaitForSecondsRealtime(0.4f);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "Quando encosto o ouvido nela, ouço um sussurro distante.",
            "O vovô disse que a árvore sugou a pureza do solo.",
            "É como se ela tivesse filtrado toda a dor deste lugar para este fruto."
        });

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;

        farmDay5Manager.MarkPineconeFound();
    }
}