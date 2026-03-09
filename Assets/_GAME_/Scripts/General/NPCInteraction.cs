using System.Collections;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogue;

    bool talking;

    public void Interact()
    {
        if (talking) return;

        talking = true;
        StartCoroutine(RunDialogue());
    }

    IEnumerator RunDialogue()
    {
        yield return dialogue.StartDialogue();
        talking = false;
    }
}