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
    [SerializeField] private GameUI gameUI;
    [SerializeField] private PlayerController player;

    [Header("Scene Objects")]
    [SerializeField] private GameObject intactRadioObject;
    [SerializeField] private GameObject brokenRadioObject;
    [SerializeField] private GameObject brokenRadioInteractable;
    [SerializeField] private GameObject whirlwindObject;
    [SerializeField] private GameObject northClueObject;
    [SerializeField] private Transform playerLookPos1;
    [SerializeField] private Transform playerMovPos1;
    [SerializeField] private Transform playerLookPos2;
    [SerializeField] private GameObject houseGrid;
    [SerializeField] private GameObject houseGridDarker;

    [Header("Whirlwind Path")]
    [SerializeField] private Transform whirlwindStartPos;
    [SerializeField] private Transform whirlwindRadioPos;
    [SerializeField] private Transform whirlwindDoorPos;
    [SerializeField] private Transform whirlwindExitPos;
    [SerializeField] private float whirlwindMoveSpeed = 6f;

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
        bool northClueRevealed = ProgressionManager.Instance.act6NorthClueRevealed;

        intactRadioObject.SetActive(!chaosPlayed);
        brokenRadioObject.SetActive(chaosPlayed);
        brokenRadioInteractable.SetActive(chaosPlayed && !noteFound);
        northClueObject.SetActive(northClueRevealed);

        houseGrid.SetActive(!chaosPlayed);
        houseGridDarker.SetActive(chaosPlayed);

        whirlwindObject.SetActive(false);
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
        gameUI.gameObject.SetActive(false);

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

        gameUI.gameObject.SetActive(true);
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

    public void RevealNorthClue()
    {
        if (ProgressionManager.Instance.act6NorthClueRevealed)
            return;

        ProgressionManager.Instance.act6NorthClueRevealed = true;
        ProgressionManager.Instance.SaveProgress();

        if (northClueObject != null)
            northClueObject.SetActive(true);
    }

    public void DisableBrokenRadioInteraction()
    {
        brokenRadioInteractable.SetActive(false);
    }
}