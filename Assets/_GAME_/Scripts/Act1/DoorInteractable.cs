using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Act1Manager actManager;
    [SerializeField] private NarrationUI narrationUI;

    [SerializeField] private AudioClip doorSound;
    

    public void Interact()
    {
        if (!ProgressionManager.Instance.LetterOpened)
        {
            narrationUI.StartCoroutine(
                narrationUI.ShowTextRoutine("Eu deveria ler a carta do vovô primeiro", null)
            );
            return;
        }

        AudioManager.Instance.PlaySFX(doorSound);
        actManager.ExitApartment();
    }
}