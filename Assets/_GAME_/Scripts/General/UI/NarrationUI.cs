using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NarrationUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private AudioClip typingSound;
    [SerializeField] private AudioSource audioSource;

    private CanvasGroup canvasGroup;
    private bool skipRequested;
    private bool isTyping;
    private string fullText;
    private NarrationSettings currentSettings;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        panel.SetActive(false);
        canvasGroup.alpha = 0f;
    }

    public IEnumerator ShowTextRoutine(string text, NarrationSettings settings, string scene = null)
    {
        GameStateManager.SetState(GameState.Narration);

        currentSettings = settings;
        fullText = text;

        panel.SetActive(true);
        textField.text = "";

        yield return ShowRoutine(scene);
    }

    public IEnumerator ShowTextRoutine(string text, string scene = null)
    {
        yield return ShowTextRoutine(text, NarrationSettings.Default, scene);
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

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < fullText.Length; i++)
        {
            if (skipRequested)
            {
                textField.text = fullText;
                break;
            }

            char character = fullText[i];

            if (character == '<')
            {
                int closingIndex = fullText.IndexOf('>', i);

                if (closingIndex != -1)
                {
                    builder.Append(fullText, i, closingIndex - i + 1);
                    textField.text = builder.ToString();
                    i = closingIndex;
                    continue;
                }
            }

            builder.Append(character);
            textField.text = builder.ToString();

            if (typingSound != null && character != ' ' && Random.value > 0.65f)
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
        GameStateManager.SetState(GameState.Gameplay);
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

        yield return 0f;

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