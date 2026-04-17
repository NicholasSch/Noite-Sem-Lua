using System.Collections;
using UnityEngine;

public class Act7SecondDigInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay4Manager farmDay4Manager;
    [SerializeField] private AudioClip diggingSound;

    private bool isRunning;

    public void Interact()
    {
        if (isRunning)
            return;

        if (!ProgressionManager.Instance.act7SecondDigRevealed)
            return;

        if (ProgressionManager.Instance.act7PocketWatchFound)
            return;

        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        isRunning = true;
        GameStateManager.SetState(GameState.Thought);

        AudioManager.Instance.PlaySFX(diggingSound);

        yield return new WaitForSecondsRealtime(3f);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> O relógio dele...",
            "\"Para minha eterna luz, Dante\".",
            "A foto da vó Lia está aqui.",
            "O jornal diz que a terra rejeita quem desafia a natureza",
            "Esse ser que vaga pelo engenho...",
            "ele não me atacou, ele me trouxe aqui.",
            "Ele estava protegendo a única coisa que ainda tinha valor para o Dante.",
            "Dona Curió usou o amor do meu avô como uma armadilha.",
            "Ela o transformou em uma casca e ficou com tudo o que eles construíram.",
            "Eu quero ir até lá...",
            "Eu quero acabar com isso agora,",
            "mas sinto meus pulmões queimarem só de olhar para aquela névoa.",
            "O vovô deixou avisos no caderno sobre proteção.",
            "Preciso descansar e me preparar...",
            "ou não passarei da entrada."
        });

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;

        farmDay4Manager.MarkPocketWatchFound();
    }
}