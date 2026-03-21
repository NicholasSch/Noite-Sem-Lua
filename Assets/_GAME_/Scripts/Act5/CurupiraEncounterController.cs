using System.Collections;
using UnityEngine;

public class CurupiraEncounterController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerController player;

    [Header("Curupira")]
    [SerializeField] private GameObject curupiraPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform vanishPoint;

    [Header("Audio")]
    [SerializeField] private AudioClip woodHit;
    [SerializeField] private AudioClip laugh;

    private static readonly string[] aterLines =
    {
        "<color=#531182>Lucas:</color> O que foi isso",

    };

    private bool hasPlayed;

    public void TriggerEncounter()
    {
        if (hasPlayed)
            return;

        StartCoroutine(PlayEncounter());
    }

    private IEnumerator PlayEncounter()
    {
        hasPlayed = true;

        GameStateManager.SetState(GameState.Cutscene);

        player.ForceFaceDown();

        yield return new WaitForSecondsRealtime(0.3f);

        GameObject curupira = Instantiate(curupiraPrefab, spawnPoint.position, Quaternion.identity);

        yield return StartCoroutine(MoveAndVanish(curupira));

        AudioManager.Instance.PlaySFX(woodHit);
        yield return new WaitForSecondsRealtime(0.2f);
        AudioManager.Instance.PlaySFX(laugh);

        yield return new WaitForSecondsRealtime(2f);

        yield return ThoughtUI.Instance.PlaySequence(aterLines);

        GameStateManager.SetState(GameState.Gameplay);
    }

    private IEnumerator MoveAndVanish(GameObject curupira)
    {
        float duration = 0.5f;
        float timer = 0f;

        Vector2 start = curupira.transform.position;
        Vector2 end = vanishPoint.position;

        SpriteRenderer sr = curupira.GetComponent<SpriteRenderer>();

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;
            curupira.transform.position = Vector2.Lerp(start, end, t);

            sr.color = new Color(1f, 1f, 1f, 1f - t);

            yield return null;
        }

        Destroy(curupira);
    }
}