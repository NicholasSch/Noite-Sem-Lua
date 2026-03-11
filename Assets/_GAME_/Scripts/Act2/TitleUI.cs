using System.Collections;
using TMPro;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private float fadeDuration = 1.2f;
    [SerializeField] private float holdDuration = 2.5f;

    public void Setup(string title, string subtitle)
    {
        titleText.text = title;
        subtitleText.text = subtitle;
    }

    public IEnumerator Play()
    {
        canvasGroup.alpha = 0f;

        yield return Fade(0f, 1f);
        yield return new WaitForSecondsRealtime(holdDuration);
        yield return Fade(1f, 0f);

        Destroy(gameObject);
    }

    private IEnumerator Fade(float start, float end)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, timer / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = end;
    }
}