using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    public GameObject panel;
    private CanvasGroup canvasGroup;
    public TextMeshProUGUI textField;

    private Coroutine currentRoutine;
    private bool isTyping;
    private string fullText;

    private DialogueSettings currentSettings;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        panel.SetActive(false);
        canvasGroup.alpha = 0f;
    }

    public IEnumerator ShowTextRoutine(string text, DialogueSettings settings, string? scene)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentSettings = settings;
        fullText = text;

        panel.SetActive(true);
        textField.text = "";

        yield return StartCoroutine(ShowRoutine(scene));
    }

    public IEnumerator ShowTextRoutine(string text, string?scene)
    {
        return ShowTextRoutine(text, DialogueSettings.Default, scene);
    }

    private IEnumerator ShowRoutine(string?scene)
    {
        Time.timeScale = 0f;

        yield return StartCoroutine(Fade(0f, 1f));
        yield return StartCoroutine(TypeText());
        yield return StartCoroutine(WaitAndClose(scene));
    }

    private IEnumerator TypeText()
    {
        isTyping = true;

        for (int i = 0; i < fullText.Length; i++)
        {
            textField.text += fullText[i];
            yield return new WaitForSecondsRealtime(currentSettings.typingSpeed);
        }

        isTyping = false;
    }

    private IEnumerator WaitAndClose(string? scene)
    {
        if (scene == null)
        {
        yield return new WaitForSecondsRealtime(currentSettings.displayDuration);

        yield return StartCoroutine(Fade(1f, 0f));

        panel.SetActive(false);
        Time.timeScale = 1f;
        }
        else
        {
        yield return new WaitForSecondsRealtime(currentSettings.displayDuration);

        yield return StartCoroutine(FadeChangeScene(1f, 0f, scene));

        panel.SetActive(false);
        Time.timeScale = 1f; 
        }
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
        private IEnumerator FadeChangeScene(float start, float end , string Scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scene);
        float elapsed = 0f;
        yield return 1f;
        while (elapsed < currentSettings.fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsed / currentSettings.fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = end;
    }
}