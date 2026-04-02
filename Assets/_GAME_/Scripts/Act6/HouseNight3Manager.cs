using System.Collections;
using UnityEngine;

public class HouseNight3Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip nightHouseAmbience;
    [SerializeField] private AudioClip radioStaticClip;
    [SerializeField] private AudioClip cleanMelodyClip;
    [SerializeField] private AudioClip whistleClip;
    [SerializeField] private AudioClip fallingObjectsClip;
    [SerializeField] private AudioClip radioCrashClip;

    [Header("Dependencies")]
    [SerializeField] private PlayerController player;

    [Header("Scene Objects")]
    [SerializeField] private GameObject intactRadioObject;

    [SerializeField] private GameObject pastRadioObject;
    [SerializeField] private GameObject brokenRadioObject;
    [SerializeField] private GameObject brokenRadioInteractable;
    [SerializeField] private GameObject whirlwindObject;
    [SerializeField] private GameObject caveClueObject;
    [SerializeField] private Transform playerLookPos1;
    [SerializeField] private Transform playerMovPos1;
    [SerializeField] private Transform playerMovPos2;
    [SerializeField] private Transform playerMovPos3;
    [SerializeField] private Transform playerMovPos4;
    [SerializeField] private Transform playerMovPos5;
    [SerializeField] private Transform playerMovPos6;
    [SerializeField] private Transform playerMovPos7;
    [SerializeField] private Transform playerLookPos2;
    [SerializeField] private GameObject houseGrid;
    [SerializeField] private GameObject houseGridDarker;

    [Header("Whirlwind Path")]
    [SerializeField] private Transform whirlwindStartPos;
    [SerializeField] private Transform whirlwindRadioPos;
    [SerializeField] private Transform whirlwindDoorPos;
    [SerializeField] private Transform whirlwindExitPos;
    [SerializeField] private float whirlwindMoveSpeed = 4.5f;

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(nightHouseAmbience);
        ApplySavedWorldState();
        StartCoroutine(SceneFlowRoutine());
    }

    private void ApplySavedWorldState()
    {
        bool chaosPlayed = ProgressionManager.Instance.act6NightChaosPlayed;
        bool noteFound = ProgressionManager.Instance.act6NoteFound;
        bool caveClueRevealed = ProgressionManager.Instance.act6CaveClueRevealed;

        intactRadioObject.SetActive(!chaosPlayed);
        brokenRadioObject.SetActive(chaosPlayed);
        brokenRadioInteractable.SetActive(chaosPlayed && !noteFound);

        houseGrid.SetActive(!chaosPlayed);
        houseGridDarker.SetActive(chaosPlayed);

        whirlwindObject.SetActive(false);

        caveClueObject.SetActive(chaosPlayed && !caveClueRevealed);
    }

    private IEnumerator SceneFlowRoutine()
    {
        if (ProgressionManager.Instance.currentDay == 3 &&
            ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Night &&
            !ProgressionManager.Instance.act6NightChaosPlayed)
        {
            yield return PlayNightChaosRoutine();
        }
    }

    private IEnumerator PlayNightChaosRoutine()
    {
        GameStateManager.SetState(GameState.Cutscene);
        GameUI.Instance.gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(1f);

        player.LookAtTarget(playerLookPos1);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> ...Que som é esse?",
            "O rádio não estava ligado."
        });

        AudioManager.Instance.PlaySFX(radioStaticClip);

        yield return new WaitForSecondsRealtime(1f);

        yield return player.MoveTo(playerMovPos1.position);
        yield return player.MoveTo(playerMovPos2.position);
        yield return player.MoveTo(playerMovPos3.position);
        yield return player.MoveTo(playerMovPos4.position);
        yield return player.MoveTo(playerMovPos5.position);
        yield return player.MoveTo(playerMovPos6.position);
        yield return player.MoveTo(playerMovPos7.position);

        player.ForceFaceUp();

        yield return new WaitForSecondsRealtime(0.6f);

        AudioManager.Instance.PlaySFX(cleanMelodyClip);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> Essa música... não pode estar tocando sozinha."
        });

        yield return new WaitForSecondsRealtime(0.6f);

        houseGrid.SetActive(false);
        houseGridDarker.SetActive(true);

        AudioManager.Instance.PlaySFX(whistleClip);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> Quem está aí?!",
            "Dona Curió? É você?"
        });

        player.LookAtTarget(playerLookPos2);

        whirlwindObject.SetActive(true);

        whirlwindObject.transform.position = whirlwindStartPos.position;

        AudioManager.Instance.PlaySFX(fallingObjectsClip);

        yield return MoveObjectTo(whirlwindObject.transform, whirlwindRadioPos.position, whirlwindMoveSpeed);

        AudioManager.Instance.PlaySFX(radioCrashClip);

        intactRadioObject.SetActive(false);
        brokenRadioObject.SetActive(true);

        AudioManager.Instance.StopMusic();

        player.ForceFaceUp();

        yield return MoveObjectTo(whirlwindObject.transform, whirlwindDoorPos.position, whirlwindMoveSpeed);

        yield return MoveObjectTo(whirlwindObject.transform, whirlwindExitPos.position, whirlwindMoveSpeed);

        whirlwindObject.SetActive(false);

        yield return ThoughtUI.Instance.PlaySequence(new string[]
        {
            "<color=#531182>Lucas:</color> Não... não...",
            "O rádio do vovô...",
            "Como é que isso aconteceu?"
        });

        ProgressionManager.Instance.act6NightChaosPlayed = true;
        ProgressionManager.Instance.SaveProgress();

        brokenRadioInteractable.SetActive(!ProgressionManager.Instance.act6NoteFound);

        GameUI.Instance.gameObject.SetActive(true);
        GameStateManager.SetState(GameState.Gameplay);
    }

    private IEnumerator MoveObjectTo(Transform objectToMove, Vector3 targetPosition, float speed)
    {
        while (Vector3.Distance(objectToMove.position, targetPosition) > 0.01f)
        {
            objectToMove.position = Vector3.MoveTowards(
                objectToMove.position,
                targetPosition,
                speed * Time.unscaledDeltaTime
            );

            yield return null;
        }

        objectToMove.position = targetPosition;
    }

    public void EnterRadioVisionState()
    {
        houseGrid.SetActive(true);
        houseGridDarker.SetActive(false);
        brokenRadioObject.SetActive(false);
        pastRadioObject.SetActive(true);
    }

    public void ExitRadioVisionState()
    {
        houseGrid.SetActive(false);
        houseGridDarker.SetActive(true);
        brokenRadioObject.SetActive(true);
        brokenRadioInteractable.SetActive(false);
        brokenRadioInteractable.SetActive(true);
        pastRadioObject.SetActive(false);
    }

    public void CaveClueInteracted()
    {
        if (ProgressionManager.Instance.act6CaveClueRevealed)
            return;

        ProgressionManager.Instance.act6CaveClueRevealed = true;
        ProgressionManager.Instance.SaveProgress();

        caveClueObject.SetActive(false);
    }

    public void DisableBrokenRadioInteraction()
    {
        ApplySavedWorldState();
    }
}