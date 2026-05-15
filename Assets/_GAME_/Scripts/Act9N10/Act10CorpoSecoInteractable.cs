using UnityEngine;

public class Act10CorpoSecoInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private CaveManager caveManager;

    public void Interact()
    {
        if (!ProgressionManager.Instance.act9Completed)
            return;

        caveManager.StartEpilogue();
    }
}