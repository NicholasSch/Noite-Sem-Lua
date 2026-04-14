using System.Collections;
using UnityEngine;

public class Act8HairInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay5Manager farmDay5Manager;

    [Header("Audio")]
    [SerializeField] private AudioClip musicBoxClip;
    [SerializeField] private AudioClip dayFarmMusic;
    [SerializeField] private AudioClip diggingSound;

    private bool isRunning;


    public void Interact()
    {
        if (isRunning)
            return;

        if (!ProgressionManager.Instance.act8RitualDone)
            return;

        if (ProgressionManager.Instance.act8HairFound)
            return;

        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        isRunning = true;
        GameStateManager.SetState(GameState.Thought);

        AudioManager.Instance.PlayMusic(musicBoxClip, 1f);

        AudioManager.Instance.PlaySFX(diggingSound);

        yield return new WaitForSecondsRealtime(2f);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> Um cacho de cabelos brancos...",
            "Com uma fita vermelha.",
            "O vovô guardou isso aqui...",
            "onde ele buscava força.",
            "Ele nunca aceitou que ela se foi.",
            "Ele viveu décadas abraçado a esse pedaço de cabelo,",
            "esperando que alguém tivesse a coragem que ele não teve.",
            "Vovô... eu entendi.",
            "O medo termina hoje.",
            "Dona Curió... você usou a dor dele para construir seu reino.",
            "Mas o Dante deixou uma herança que você não previu:",
            "a verdade.",
        });

        AudioManager.Instance.PlayMusic(dayFarmMusic);

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;

        farmDay5Manager.MarkHairFound();
    }
}