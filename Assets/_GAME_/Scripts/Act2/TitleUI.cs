using System.Collections;
using TMPro;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1.2f;
    [SerializeField] private float holdDuration = 10f;


    public IEnumerator Play()
    {
        canvasGroup.alpha = 0f;

        yield return Fade(0f, 2f);
        yield return new WaitForSecondsRealtime(holdDuration);
        yield return Fade(2f, 0f);

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