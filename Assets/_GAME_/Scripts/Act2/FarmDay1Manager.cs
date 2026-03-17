using System.Collections;
using UnityEngine;

public class FarmDay1Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dayFarmMusic;
    [SerializeField] private AudioClip dayFarmAmbience;

    [Header("Objects")]
    [SerializeField] private GameObject donaCurioObject;
    [SerializeField] private GameObject donaCurioTrigger;
    [SerializeField] private GameObject barnToolsObject;
    [SerializeField] private GameObject millInteractableObject;
    [SerializeField] private GameObject porchTriggerObject;

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(dayFarmAmbience);
        AudioManager.Instance.PlayMusic(dayFarmMusic);

        ApplySavedWorldState();
        StartCoroutine(SceneFlowRoutine());
    }

    public void ApplySavedWorldState()
    {
        donaCurioObject.SetActive(!ProgressionManager.Instance.act2CurioEncounterPlayed);
        donaCurioTrigger.SetActive(!ProgressionManager.Instance.act2CurioEncounterPlayed);
        barnToolsObject.SetActive(!TaskManager.Instance.IsCompleted("Barn_Tools"));
        millInteractableObject.SetActive(!TaskManager.Instance.IsCompleted("Mill_Gears"));
        porchTriggerObject.SetActive(!ProgressionManager.Instance.porchScenePlayed);
    }

    public void CompleteBarnTools()
    {
        if (TaskManager.Instance.IsCompleted("Barn_Tools"))
            return;

        TaskManager.Instance.CompleteTask("Barn_Tools");
        ApplySavedWorldState();
    }

    public void CompleteMillGears()
    {
        if (TaskManager.Instance.IsCompleted("Mill_Gears"))
            return;

        TaskManager.Instance.CompleteTask("Mill_Gears");
        ApplySavedWorldState();
    }

    public void MarkCurioEncounterPlayed()
    {
        if (ProgressionManager.Instance.act2CurioEncounterPlayed)
            return;

        ProgressionManager.Instance.act2CurioEncounterPlayed = true;
        ProgressionManager.Instance.RegisterNpcTalk("CucaDisguised");
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    private IEnumerator SceneFlowRoutine()
    {
        if (ProgressionManager.Instance.currentDay == 1 &&
            ProgressionManager.Instance.currentPeriod == ProgressionManager.DayPeriod.Day &&
            !ProgressionManager.Instance.farmIntroPlayed)
        {
            yield return PlayFarmIntro();
        }
    }

    private IEnumerator PlayFarmIntro()
    {
        yield return new WaitForSecondsRealtime(1f);

        string[] lines =
        {
            "<color=#531182>Lucas:</color> Droga... o motor ferveu bem na entrada.",
            "Pelo menos eu já trouxe o caderno do vovô.",
            "Parece que vou ter tempo de sobra para ler enquanto esse ferro velho esfria."
        };

        yield return ThoughtUI.Instance.PlaySequence(lines);

        ProgressionManager.Instance.farmIntroPlayed = true;
        ProgressionManager.Instance.SaveProgress();
    }
}