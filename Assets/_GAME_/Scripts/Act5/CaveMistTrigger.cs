using System.Collections;
using UnityEngine;

public class CaveMistTrigger : MonoBehaviour
{
    [SerializeField] private Transform returnPoint;
    [SerializeField] private CanvasGroup whiteFlashCanvas;
    [SerializeField] private AudioClip coughSound;

    private bool isRunning;

    private static readonly string[] FirstLines =
    {
        "<color=#531182>Lucas:</color> O jornal mencionou uma Garganta da Rocha...",
        "Mas a neblina aqui está tão espessa que nem a luz da lanterna atravessa.",
        "E esse cheiro de coisa queimada... é insuportável.",
        "Melhor eu voltar pra casa principal enquanto ainda consigo ver meus próprios pés."
    };

    private static readonly string[] RepeatLines =
    {
        "<color=#531182>Lucas:</color> Não consigo respirar aqui.",
        "Preciso de algo pra filtrar esse ar... ou esperar o vento mudar."
    };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (isRunning)
            return;

        StartCoroutine(BlockRoutine(other.transform));
    }

    private IEnumerator BlockRoutine(Transform playerTransform)
    {
        isRunning = true;

        GameStateManager.SetState(GameState.Cutscene);

        yield return FlashWhite(0f, 1f, 0.25f);

        if (coughSound != null)
            AudioManager.Instance.PlaySFX(coughSound);

        yield return new WaitForSecondsRealtime(0.4f);

        playerTransform.position = returnPoint.position;

        yield return FlashWhite(1f, 0f, 0.4f);

        if (!ProgressionManager.Instance.act5CaveBlockedSeen)
        {
            yield return ThoughtUI.Instance.PlaySequence(FirstLines);
            ProgressionManager.Instance.act5CaveBlockedSeen = true;
            ProgressionManager.Instance.SaveProgress();
        }
        else
        {
            yield return ThoughtUI.Instance.PlaySequence(RepeatLines);
        }

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;
    }

    private IEnumerator FlashWhite(float start, float end, float duration)
    {
        float timer = 0f;
        whiteFlashCanvas.alpha = start;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            whiteFlashCanvas.alpha = Mathf.Lerp(start, end, timer / duration);
            yield return null;
        }

        whiteFlashCanvas.alpha = end;
    }
}