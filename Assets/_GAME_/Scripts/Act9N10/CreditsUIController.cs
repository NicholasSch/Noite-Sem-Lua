using System.Collections;
using UnityEngine;

public class CreditsUIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 2.5f;

    private void OnEnable()
    {
        canvasGroup.alpha = 0f;
        StartCoroutine(FadeInCredits());
    }

    private IEnumerator FadeInCredits()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
}