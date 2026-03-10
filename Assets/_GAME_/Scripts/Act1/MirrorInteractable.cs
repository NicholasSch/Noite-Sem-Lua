using System;
using UnityEngine;

public class MirrorInteractable : MonoBehaviour, IInteractable
{
    public NarrationUI narrationUI;

    public void Interact()
    {
    narrationUI.StartCoroutine(
        narrationUI.ShowTextRoutine("Você vê seu reflexo no espelho. Você Parece exausto.",null)
    );
    }
}