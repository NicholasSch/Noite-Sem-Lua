using UnityEngine;

public class Act9CucaEncounterTrigger : MonoBehaviour
{
    [SerializeField] private CaveManager caveManager;
    private bool hasTriggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;
        caveManager.StartFinalEncounter();
    }
}