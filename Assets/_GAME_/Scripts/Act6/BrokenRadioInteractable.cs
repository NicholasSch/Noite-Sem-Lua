using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BrokenRadioInteractable : MonoBehaviour, IInteractable
{
    [Header("Dependencies")]
    [SerializeField] private HouseNight3Manager houseNight3Manager;

    [Header("Vision Objects")]
    [SerializeField] private GameObject danteSilhouetteObject;
    [SerializeField] private GameObject liaSilhouetteObject;
    [SerializeField] private NPCController danteSilhouetteController;
    [SerializeField] private NPCController liaSilhouetteController;
    [SerializeField] private Transform danteStepForwardTarget;

    [Header("Audio")]
    [SerializeField] private AudioClip cleanMelodyClip;
    [SerializeField] private AudioClip paperCrumpleClip;

    private bool isRunning;

    public void Interact()
    {
        if (isRunning)
            return;

        if (!ProgressionManager.Instance.act6NightChaosPlayed)
            return;

        if (!ProgressionManager.Instance.act6RadioVisionSeen)
        {
            StartCoroutine(PlayVisionRoutine());
            return;
        }

        if (!ProgressionManager.Instance.act6NoteFound)
        {
            StartCoroutine(FindNoteRoutine());
        }
    }

    private IEnumerator PlayVisionRoutine()
    {
        isRunning = true;
        GameStateManager.SetState(GameState.Cutscene);

        houseNight3Manager.EnterRadioVisionState();

        danteSilhouetteObject.SetActive(true);
        danteSilhouetteController.FaceDirection(NPCController.Direction.Down);
        liaSilhouetteObject.SetActive(true);
        liaSilhouetteController.FaceDirection(NPCController.Direction.Up);

        AudioManager.Instance.PlayMusic(cleanMelodyClip, 1.5f);

        yield return new WaitForSecondsRealtime(0.5f);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#A92F87>Lia:</color> Dante, olhe para este lugar...",
            "As sombras estão ficando mais longas.",
            "Eu sinto que o Engenho está me chamando para o chão.",
            "<color=#8C6B3B>Dante:</color> Não enquanto eu tiver voz, Lia.",
            "Se a Dona Curió quer silêncio, eu vou dar a ela o que ela pede...",
            "mas vou esconder nossa música onde ela nunca vai procurar.",
            "<color=#A92F87>Lia:</color> Você faria isso?",
            "Viver no silêncio por mim?",
            "<color=#8C6B3B>Dante:</color> Eu viveria mil anos no escuro", 
            "se isso garantisse que o nosso neto ouvisse essa música um dia.",
            "Ela é o nosso mapa de volta."
        });

        yield return danteSilhouetteController.WalkTo(danteStepForwardTarget.position);

        AudioManager.Instance.PlaySFX(paperCrumpleClip);

        yield return new WaitForSecondsRealtime(1f);

        AudioManager.Instance.StopMusic(1.5f);

        yield return new WaitForSecondsRealtime(1f);

        danteSilhouetteObject.SetActive(false);
        liaSilhouetteObject.SetActive(false);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> ...O vovô escondeu alguma coisa aí dentro?"
        });

        ProgressionManager.Instance.act6RadioVisionSeen = true;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;

        houseNight3Manager.ExitRadioVisionState();
    }

    private IEnumerator FindNoteRoutine()
    {
        isRunning = true;
        GameStateManager.SetState(GameState.Thought);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> Tem mesmo um bilhete preso aqui..."
        });

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "Bilhete de Dante:",
            "Lia, se a música parar, não se assuste.",
            "Eu fui buscar o silêncio para que você pudesse descansar.",
            "O preço foi alto", 
            "mas o Engenho guardará nossa história até que o nosso sangue volte para reclamá-la."
        });

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> Ele trocou a própria voz pela paz dela.",
            "O pacto com a Cuca... começou aqui, por amor.",
            "O Saci não quebrou o rádio para me machucar.",
            "Ele quebrou o rádio para libertar essa mensagem que estava presa no silêncio há décadas."
        });

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> Eles não mereciam isso.",
            "Eu vou descobrir o que a Dona Curió está escondendo naquela caverna", 
            "nem que eu tenha que enfrentar o próprio vento."
        });

        ProgressionManager.Instance.act6NoteFound = true;
        ProgressionManager.Instance.SaveProgress();

        GameStateManager.SetState(GameState.Gameplay);
        isRunning = false;

        houseNight3Manager.DisableBrokenRadioInteraction();
    }
}