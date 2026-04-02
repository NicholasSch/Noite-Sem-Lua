using UnityEngine;

public class MirrorInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        StartCoroutine(NarrationUI.Instance.ShowTextRoutine("Você vê seu reflexo no espelho. Você parece exausto.", null));
    }
}