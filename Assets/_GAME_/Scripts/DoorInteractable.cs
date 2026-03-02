using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    public Act1Manager actManager;

    public void Interact()
    {
        actManager.ExitApartment();
    }
}