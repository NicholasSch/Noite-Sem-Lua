using UnityEngine;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    [TextArea(3,10)]
    public string[] lines;

    public IEnumerator StartDialogue()
    {
        yield return ThoughtUI.Instance.PlaySequence(lines);
    }
}