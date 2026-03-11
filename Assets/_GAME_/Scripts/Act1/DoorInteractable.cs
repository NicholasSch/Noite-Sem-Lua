using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Act1Manager actManager;
    [SerializeField] private NarrationUI narrationUI;

    public void Interact()
    {
        if (!ProgressionManager.Instance.LetterOpened)
        {
            narrationUI.StartCoroutine(
                narrationUI.ShowTextRoutine("Eu deveria ler a carta do vovô primeiro", null)
            );
            return;
        }

        actManager.ExitApartment();
    }
}