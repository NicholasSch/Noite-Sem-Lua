using System.Collections;
using UnityEngine;

public class BenchVisionInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private BenchVisionCutsceneController benchVisionController;

    public void Interact()
    {
        if (ProgressionManager.Instance.act3BenchVisionSeen)
            return;

        StartCoroutine(benchVisionController.PlayVision());
    }
}