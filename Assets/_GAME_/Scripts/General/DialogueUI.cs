using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI textField;
    public AudioClip typingSound;

    private CanvasGroup canvasGroup;
    public AudioSource audioSource;

    private bool skipRequested;
    private bool isTyping;

    private string fullText;
    private DialogueSettings currentSettings;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        panel.SetActive(false);
        canvasGroup.alpha = 0f;
    }

    public IEnumerator ShowTextRoutine(string text, DialogueSettings settings, string scene = null)
    {
        GameStateManager.CurrentState = GameState.Dialogue;

        currentSettings = settings;
        fullText = text;

        panel.SetActive(true);
        textField.text = "";

        yield return StartCoroutine(ShowRoutine(scene));
    }

    public IEnumerator ShowTextRoutine(string text, string scene = null)
    {
        return ShowTextRoutine(text, DialogueSettings.Default, scene);
    }

    private IEnumerator ShowRoutine(string scene)
    {
        Time.timeScale = 0f;

        yield return Fade(0f, 1f);
        yield return TypeText();
        yield return WaitAndClose(scene);
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        skipRequested = false;

        textField.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            if (skipRequested)
            {
                textField.text = fullText;
                break;
            }

            char letter = fullText[i];
            textField.text += letter;

            if (typingSound != null && letter != ' ' && Random.value > 0.5f)
            {
                audioSource.PlayOneShot(typingSound, 0.4f);
            }

            yield return new WaitForSecondsRealtime(currentSettings.typingSpeed);
        }

        isTyping = false;
    }

    private IEnumerator WaitAndClose(string scene)
    {
        yield return new WaitForSecondsRealtime(currentSettings.displayDuration);

        if (scene == null)
        {
            yield return Fade(1f, 0f);
        }
        else
        {
            yield return FadeAndChangeScene(scene);
        }

        panel.SetActive(false);
        Time.timeScale = 1f;
        GameStateManager.CurrentState = GameState.Gameplay;
    }

    private IEnumerator Fade(float start, float end)
    {
        float elapsed = 0f;

        while (elapsed < currentSettings.fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsed / currentSettings.fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = end;
    }

    private IEnumerator FadeAndChangeScene(string scene)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(scene);
        load.allowSceneActivation = false;

        yield return Fade(1f, 0f);

        load.allowSceneActivation = true;
    }

    public void OnSubmit()
    {
        if (isTyping)
        {
            skipRequested = true;
        }
    }
}