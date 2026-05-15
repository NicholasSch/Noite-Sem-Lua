using System.Collections;
using UnityEngine;
public class CaveManager : MonoBehaviour
{

    [Header("Dependencies")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject cucaObject;
    [SerializeField] private GameObject cucaEncounterTrigger;
    [SerializeField] private GameObject corpoSecoObject;
    [SerializeField] private GameObject corpoSecoInteractable;
    [SerializeField] private GameObject danteSpirit;
    [SerializeField] private GameObject liaSpirit;
    [SerializeField] private GameObject mistSpawner;

    [Header("Audio")]
    [SerializeField] private AudioClip cucaScream;
    [SerializeField] private AudioClip breakRootsSFX;
    [SerializeField] private AudioClip CorpoSecoTransformation;
    [SerializeField] private AudioClip finalTheme;
    [SerializeField] private AudioClip nightMusic;

    [Header("Cutscene")]
    [SerializeField] private Transform playerMovePos1;
    [SerializeField] private Transform playerMovePos2;
    [SerializeField] private Transform playerMovePos3;
    [SerializeField] private Transform exitPos;

    private void Start()
    {   

        AudioManager.Instance.PlayMusic(nightMusic);
        
        playerController.SetSpeedMultiplier(0.9f);
        cucaObject.SetActive(!ProgressionManager.Instance.act9Completed);
        cucaEncounterTrigger.SetActive(!ProgressionManager.Instance.act9Completed);
        corpoSecoInteractable.SetActive(ProgressionManager.Instance.act9Completed && !ProgressionManager.Instance.act10Started);
        mistSpawner.SetActive(!ProgressionManager.Instance.act9Completed);
        danteSpirit.SetActive(false);
        liaSpirit.SetActive(false);
    }

    public void StartFinalEncounter()
    {
        cucaEncounterTrigger.SetActive(false);
        StartCoroutine(FinalCucaSequence());
    }

    private IEnumerator FinalCucaSequence()
    {
        GameStateManager.SetState(GameState.Cutscene);

        mistSpawner.SetActive(false);

       AudioManager.Instance.StopMusic(4f);
        yield return ThoughtUI.Instance.PlaySequence(new string[] {
            "<color=#67D221>Cuca:</color> Você é persistente, pequeno herdeiro.",
            "Dante me deu o seu silêncio para que ela não sofresse.",
            "O que você tem para me oferecer em troca da alma dele?",
            "<color=#531182>Lucas:</color> Eu não vim negociar. Eu vim devolver o que o medo roubou."
        });

        AudioManager.Instance.PlayMusic(finalTheme);
        yield return playerController.MoveTo(playerMovePos1.position);
        yield return ThoughtUI.Instance.PlaySequence(new string[] {
            "<color=#531182>Lucas:</color> Eu trouxe o relógio dele.", 
            "Ele ainda marca o tempo de uma promessa que você não conseguiu apagar."
        });

        yield return playerController.MoveTo(playerMovePos2.position);
        playerController.ForceFaceUp();
        yield return ThoughtUI.Instance.PlaySequence(new string[] {
            "<color=#531182>Lucas:</color> E eu trouxe a lembrança da Lia. O amor deles não é uma dívida que você pode cobrar."
        });

        yield return playerController.MoveTo(playerMovePos3.position);
        playerController.ForceFaceUp();
        yield return ThoughtUI.Instance.PlaySequence(new string[] {
            "<color=#531182>Lucas:</color> O vovô deixou um recado para você, Cuca.",
            "Ele disse que o amor não é uma dívida... é uma libertação!"
        });
        AudioManager.Instance.PlaySFX(breakRootsSFX);

        yield return ThoughtUI.Instance.PlaySequence(new string[] {
            "<color=#67D221>Cuca:</color> O Engenho... ele voltará a ser apenas terra e silêncio... sem mim, não há nada!",
            "<color=#531182>Lucas:</color> Antes o silêncio da paz do que o barulho da sua mentira."
        });
        AudioManager.Instance.PlaySFX(cucaScream);

        FadeOut(cucaObject,1f);
        
        TaskManager.Instance.CompleteTask("Act9_DefeatCurio");
        ProgressionManager.Instance.act9Completed = true;
        corpoSecoInteractable.SetActive(true);
        GameStateManager.SetState(GameState.Gameplay);
    }

    public void StartEpilogue()
    {
        StartCoroutine(EpilogueSequence());
    }

    private IEnumerator EpilogueSequence()
    {
        GameStateManager.SetState(GameState.Cutscene);
        corpoSecoInteractable.SetActive(false);
        
        AudioManager.Instance.PlaySFX(CorpoSecoTransformation);
        yield return new WaitForSeconds(0.5f);
        corpoSecoObject.SetActive(false);
        danteSpirit.SetActive(true);
        yield return new WaitForSeconds(1f);

        liaSpirit.SetActive(true);

        yield return ThoughtUI.Instance.PlaySequence(new string[] {
            "<color=#8C6B3B>Dante:</color> Lia...? Eu... eu tentei segurar o tempo. Eu só queria mais um minuto.",
            "<color=#A92F87>Lia:</color> O tempo agora é nosso, Dante. Para sempre.",
            "Você não precisa mais carregar o peso deste chão."
        });


        StartCoroutine(FadeOut(danteSpirit,3f));
        yield return FadeOut(liaSpirit,3f);

        danteSpirit.SetActive(false);
        liaSpirit.SetActive(false);

        yield return ThoughtUI.Instance.PlaySequence(new string[] {
            "<color=#531182>Lucas:</color> Adeus, vovô. A promessa está cumprida."
        });

        ProgressionManager.Instance.act10Started = true;
        GameStateManager.SetState(GameState.Gameplay);
    }

    private IEnumerator FadeOut(GameObject character, float divider)
    {
        float alpha = 1f;

        SpriteRenderer characterSR = character.GetComponentInChildren<SpriteRenderer>();

        while (alpha > 0)
        {
            alpha -= Time.deltaTime/divider;
            characterSR.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        
        character.SetActive(false);
    }
}