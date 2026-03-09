using System;
using UnityEngine;

public class MirrorInteractable : MonoBehaviour, IInteractable
{
    public DialogueUI dialogueUI;

    public void Interact()
    {
    dialogueUI.StartCoroutine(
        dialogueUI.ShowTextRoutine("Você vê seu reflexo no espelho. Você Parece exausto.",null)
    );
    }
}