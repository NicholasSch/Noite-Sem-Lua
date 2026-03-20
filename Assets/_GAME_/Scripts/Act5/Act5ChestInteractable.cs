using System.Collections;
using UnityEngine;

public class Act5ChestInteractable : MonoBehaviour, IInteractable
{   
    [Header("Audio")]
    [SerializeField] private AudioClip chestOpenSound;
    [SerializeField] private AudioClip paperPickupSound;
    
    [Header("Sprites")]
    [SerializeField] private GameObject chestClosedObject;
    [SerializeField] private GameObject chestOpenFullObject;
    [SerializeField] private GameObject chestOpenEmptyObject;


    private static readonly string[] ChestLines =
    {
        "<color=#531182>Lucas:</color> Não tem lanterna aqui...",
        "Mas tem um pacote embrulhado em papel pardo.",
        "“Para quem guarda as trilhas, o fumo é o melhor cumprimento.”",
        "<color=#531182>Lucas:</color> O vovô guardava isso como se fosse um tesouro... fumo de rolo?",
        "Ele sempre dizia que a mata tem seus donos e que é bom ser educado com eles.",
        "Melhor eu levar. Do jeito que as coisas andam aqui, qualquer cuidado é pouco."
    };

    private bool isRunning;

    private void Start()
    {
        ApplyVisualState();
    }

    public void Interact()
    {
        if (isRunning)
            return;

        if (ProgressionManager.Instance.act5TobaccoFound)
            return;

        StartCoroutine(InteractionRoutine());
    }

    private IEnumerator InteractionRoutine()
    {
        isRunning = true;

        GameStateManager.SetState(GameState.Thought);

        AudioManager.Instance.PlaySFX(chestOpenSound);

        chestClosedObject.SetActive(false);
        chestOpenFullObject.SetActive(true);
        chestOpenEmptyObject.SetActive(false);

        yield return new WaitForSecondsRealtime(0.8f);

        yield return ThoughtUI.Instance.PlaySequence(ChestLines);

        AudioManager.Instance.PlaySFX(paperPickupSound);

        ProgressionManager.Instance.act5TobaccoFound = true;
        ProgressionManager.Instance.SaveProgress();

        chestClosedObject.SetActive(false);
        chestOpenFullObject.SetActive(false);
        chestOpenEmptyObject.SetActive(true);

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;
    }

    private void ApplyVisualState()
    {
        if (ProgressionManager.Instance.act5TobaccoFound)
        {
            chestClosedObject.SetActive(false);
            chestOpenFullObject.SetActive(false);
            chestOpenEmptyObject.SetActive(true);
            return;
        }

        chestClosedObject.SetActive(true);
        chestOpenFullObject.SetActive(false);
        chestOpenEmptyObject.SetActive(false);
    }
}