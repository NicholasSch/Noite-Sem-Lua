using UnityEngine;

public class Act4CurioEncounterTrigger : MonoBehaviour
{
    [SerializeField] private Act4CurioEncounterController controller;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!TaskManager.Instance.IsCompleted("Trail_Marker"))
            return;

        if (!TaskManager.Instance.IsCompleted("Market_Supplies"))
            return;

        controller.TriggerEncounter();
    }
}