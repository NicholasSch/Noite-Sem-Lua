using System.Collections;
using UnityEngine;

public class Act7MillMessageInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay4Manager farmDay4Manager;

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

        if (!ProgressionManager.Instance.act7NewspaperFound)
            return;

        if (ProgressionManager.Instance.act7MillMessageFound)
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
            "Há um entalhe na madeira...",
            "As letras estão gastas.",
            "Mas ainda dá para ler.",
            "\"Lia, eu transformei o vento em música,",
            "mas agora o vento só traz silêncio.",
            "Perdoe-me por não saber deixá-la ir.\"",
            "<color=#531182>Lucas:</color> Ele não conseguia aceitar o fim...",
            "O vovô se culpava tanto que permitiu que esse lugar morresse junto com ela.",
            "Esse arrependimento...",
            "é isso que está secando o Engenho."
        });

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;

        farmDay4Manager.MarkAct7MillMessageFound();
    }
}