using System.Collections;
using UnityEngine;
using TMPro;

public class ThoughtUI : MonoBehaviour
{

    public AudioClip typingSound;
    public static ThoughtUI Instance;

    public GameObject panel;
    public TextMeshProUGUI textField;

    public float typingSpeed = 0.03f;
    public float displayTime = 3f;

    Coroutine routine;
    bool skip;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowThought(string text)
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(Run(text));
    }

    IEnumerator Run(string text)
    {
        panel.SetActive(true);
        skip = false;

        textField.text = text;
        textField.ForceMeshUpdate();

        int totalCharacters = textField.textInfo.characterCount;
        textField.maxVisibleCharacters = 0;

        while (textField.maxVisibleCharacters < totalCharacters)
        {
            if (skip)
            {
                textField.maxVisibleCharacters = totalCharacters;
                break;
            }

            textField.maxVisibleCharacters++;

            if (typingSound != null)
                AudioManager.Instance.PlayUI(typingSound);

            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        yield return new WaitForSecondsRealtime(displayTime);

        panel.SetActive(false);
    }

    public void Skip()
    {
        skip = true;
    }
}