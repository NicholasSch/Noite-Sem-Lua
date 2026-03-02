using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentInteractable = other.GetComponent<IInteractable>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<IInteractable>() != null)
            currentInteractable = null;
    }

    private void OnInteract(InputValue value)
    {
        if (value.isPressed && currentInteractable != null)
            currentInteractable.Interact();
    }
}