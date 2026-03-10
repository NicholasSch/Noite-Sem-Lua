using System.Collections;
using UnityEngine;
using TMPro;

public class ThoughtUI : MonoBehaviour
{
    public static ThoughtUI Instance;

    public GameObject panel;
    public TextMeshProUGUI textField;

    public AudioClip typingSound;

    public float typingSpeed = 0.03f;
    public float displayTime = 1f;

    bool skip;
    Coroutine routine;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    // Plays a sequence of thoughts
    public IEnumerator PlaySequence(string[] lines)
    {
        if (routine != null)
            StopCoroutine(routine);

        panel.SetActive(true);

        for (int i = 0; i < lines.Length; i++)
        {
            yield return RunLine(lines[i]);
        }

        panel.SetActive(false);
    }

    IEnumerator RunLine(string text)
    {
        skip = false;

        textField.text = text;
        textField.ForceMeshUpdate();

        int totalCharacters = textField.textInfo.characterCount;
        textField.maxVisibleCharacters = 0;

        int soundCounter = 0;

        while (textField.maxVisibleCharacters < totalCharacters)
        {
            if (skip)
            {
                textField.maxVisibleCharacters = totalCharacters;
                break;
            }

            textField.maxVisibleCharacters++;

            soundCounter++;

            if (typingSound != null && Random.value > 0.65f)
            {
                AudioManager.Instance.PlayUI(typingSound);
            }

            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        yield return new WaitForSecondsRealtime(displayTime);
    }

    public void Skip()
    {
        skip = true;
    }
}