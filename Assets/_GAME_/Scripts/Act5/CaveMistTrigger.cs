using System.Collections;
using UnityEngine;

public class CaveMistTrigger : MonoBehaviour
{
    [SerializeField] private Transform returnPoint;
    [SerializeField] private AudioClip coughSound;
    [SerializeField] private PlayerController player;

    private bool isRunning;

    private static readonly string[] genericLines =
    {
        "<color=#531182>Lucas:</color> Essa neblina está estranha...",
        "Nem a luz da lanterna atravessa direito.",
        "E esse cheiro... parece coisa queimada.",
        "Melhor eu sair daqui."
    };

    private static readonly string[] newspaperReadLines =
    {
        "<color=#531182>Lucas:</color> O jornal mencionou uma Garganta da Rocha...",
        "Mas a neblina aqui está tão espessa que nem a luz da lanterna atravessa.",
        "E esse cheiro de coisa queimada... é insuportável.",
        "Melhor eu voltar pra casa principal enquanto ainda consigo ver meus próprios pés."
    };

    private static readonly string[] repeatLines =
    {
        "<color=#531182>Lucas:</color> Não consigo respirar aqui.",
        "Preciso de algo pra filtrar esse ar... ou esperar o vento mudar."
    };

    private void Start()
    {
        if (player == null)
            player = FindFirstObjectByType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (isRunning)
            return;

        StartCoroutine(BlockRoutine());
    }

    private IEnumerator BlockRoutine()
    {
        isRunning = true;

        GameStateManager.SetState(GameState.Cutscene);

        AudioManager.Instance.PlaySFX(coughSound);

        yield return new WaitForSecondsRealtime(0.25f);

        yield return player.MoveTo(returnPoint.position, 2f);

        GameStateManager.SetState(GameState.Thought);

        if (!ProgressionManager.Instance.act5NewspaperFound)
        {
            yield return ThoughtUI.Instance.PlaySequence(genericLines);
        }
        else if (!ProgressionManager.Instance.act5CaveBlockedSeen)
        {
            yield return ThoughtUI.Instance.PlaySequence(newspaperReadLines);
            ProgressionManager.Instance.act5CaveBlockedSeen = true;
            ProgressionManager.Instance.SaveProgress();
        }
        else
        {
            yield return ThoughtUI.Instance.PlaySequence(repeatLines);
        }

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;
    }
}