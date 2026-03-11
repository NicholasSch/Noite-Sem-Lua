using System.Collections;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField] private string[] lines;

    public IEnumerator StartDialogue()
    {
        yield return ThoughtUI.Instance.PlaySequence(lines);
    }
}