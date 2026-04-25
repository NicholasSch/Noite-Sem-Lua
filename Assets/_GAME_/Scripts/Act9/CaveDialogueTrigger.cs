using UnityEngine;

public class CaveDialogueTrigger : MonoBehaviour
{
    [SerializeField] private string[] dialogueLines;
    private bool hasTriggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;
        StartCoroutine(PlayDialogue());
    }

    private System.Collections.IEnumerator PlayDialogue()
    {
        yield return ThoughtUI.Instance.PlaySequence(dialogueLines);
        Destroy(gameObject);
    }
}