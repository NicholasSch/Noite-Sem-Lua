using UnityEngine;

public class MirrorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private NarrationUI narrationUI;

    public void Interact()
    {
        narrationUI.StartCoroutine(
            narrationUI.ShowTextRoutine("Você vê seu reflexo no espelho. Você parece exausto.", null)
        );
    }
}