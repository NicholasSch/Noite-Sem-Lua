using UnityEngine;

public class Act7CorpoSecoEncounterTrigger : MonoBehaviour
{
    [SerializeField] private Act7CorpoSecoEncounterController controller;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!ProgressionManager.Instance.act7MillMessageFound)
            return;

        if (ProgressionManager.Instance.act7SecondDigRevealed)
            return;

        controller.TriggerEncounter();
    }
}