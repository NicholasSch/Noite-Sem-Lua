using System.Collections;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [System.Serializable]
    public class DialogueBlock
    {
        [TextArea(3, 10)]
        public string[] lines;
    }

    [SerializeField] private DialogueBlock[] dialogueBlocks;

    public IEnumerator PlayDialogueBlock(int index)
    {
        if (index < 0 || index >= dialogueBlocks.Length)
            yield break;

        string[] lines = dialogueBlocks[index].lines;

        if (lines == null || lines.Length == 0)
            yield break;

        yield return ThoughtUI.Instance.PlaySequence(lines);
    }

    public int GetDialogueBlockCount()
    {
        return dialogueBlocks != null ? dialogueBlocks.Length : 0;
    }
}