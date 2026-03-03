using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    public Act1Manager actManager;

    public DialogueUI dialogueUI;

    public void Interact()
    {
    if (!GameStateManager.LetterWasRead){
        dialogueUI.StartCoroutine(
        dialogueUI.ShowTextRoutine("Eu deveria ler a carta do vovô primeiro",null)
    );
    return;
    }
    actManager.ExitApartment();
    }
}