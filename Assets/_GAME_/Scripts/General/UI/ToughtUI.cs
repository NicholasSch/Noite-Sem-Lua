using System.Collections;
using TMPro;
using UnityEngine;

public class ThoughtUI : MonoBehaviour
{
    public static ThoughtUI Instance { get; private set; }

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private AudioClip typingSound;
    [SerializeField] private float typingSpeed = 0.03f;
    [SerializeField] private float displayTime = 1f;

    private bool skip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            panel.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator PlaySequence(string[] lines)
    {
        panel.SetActive(true);

        for (int i = 0; i < lines.Length; i++)
        {
            yield return RunLine(lines[i]);
        }

        panel.SetActive(false);
    }

    private IEnumerator RunLine(string text)
    {
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