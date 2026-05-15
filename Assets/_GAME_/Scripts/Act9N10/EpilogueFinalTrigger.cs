using UnityEngine;

public class EpilogueFinalTrigger : MonoBehaviour
{
    [SerializeField] private FarmEpilogueManager epilogueManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            epilogueManager.TriggerFinalSequence();
        }
    }
}