using UnityEngine;

public class Act2CurioEncounterTrigger : MonoBehaviour
{
    [SerializeField] private Act2CurioEncounterController controller;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (ProgressionManager.Instance.act2CurioEncounterPlayed)
            return;

        controller.TriggerEncounter();
    }
}