using System.Collections;
using UnityEngine;

public class FarmDay2Manager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dayFarmMusic;
    [SerializeField] private AudioClip dayFarmAmbience;

    [Header("Dependencies")]
    [SerializeField] private GameUI gameUI;

    [Header("Act 3 Objects")]
    [SerializeField] private GameObject vegetationGrid;
    [SerializeField] private GameObject vegetationGrid2;
    [SerializeField] private GameObject saplingInteractable;
    [SerializeField] private GameObject benchInteractable;
    [SerializeField] private GameObject lakeInteractable;
    [SerializeField] private GameObject newspaperInteractable;
    [SerializeField] private GameObject plantedTreeObject;

    [Header("Act 4 Objects")]
    [SerializeField] private GameObject dirtyMarkerObject;
    [SerializeField] private GameObject cleanMarkerObject;
    [SerializeField] private GameObject curioEncounterTrigger;
    [SerializeField] private GameObject act4Curio;

    private void Start()
    {
        ApplySavedWorldState();

        gameUI.gameObject.SetActive(!ProgressionManager.Instance.act4HideGameUI);

        string[] intro =
        {
            "<color=#531182>Lucas:</color> Um novo dia."
        };

        if (!ProgressionManager.Instance.act3BenchVisionSeen)
        {
            StartCoroutine(ThoughtUI.Instance.PlaySequence(intro));
        }
        StartCoroutine(StartSequence());
    }

    public void ApplySavedWorldState()
    {
        ApplyAct3State();
        ApplyAct4State();
    }

    private IEnumerator StartSequence()
    {
        AudioManager.Instance.PlayAmbient(dayFarmAmbience);

        yield return new WaitForSecondsRealtime(3f);
        AudioManager.Instance.PlayMusic(dayFarmMusic);
        yield return new WaitForSecondsRealtime(1f);
        
    }

    private void ApplyAct3State()
    {
        bool orchardDone = TaskManager.Instance.IsCompleted("Orchard_Care");
        bool benchVisionSeen = ProgressionManager.Instance.act3BenchVisionSeen;
        bool plantDone = TaskManager.Instance.IsCompleted("Plant_Hope");
        bool lakeToolDone = TaskManager.Instance.IsCompleted("Lake_Toll");
        bool newspaperFound = ProgressionManager.Instance.act3NewspaperFound;

        vegetationGrid.SetActive(!orchardDone);
        vegetationGrid2.SetActive(orchardDone);
        benchInteractable.SetActive(!benchVisionSeen);
        saplingInteractable.SetActive(benchVisionSeen && !plantDone);
        lakeInteractable.SetActive(!lakeToolDone);
        newspaperInteractable.SetActive(!newspaperFound);
        plantedTreeObject.SetActive(plantDone);
    }

    private void ApplyAct4State()
    {
        bool markerDone = TaskManager.Instance.IsCompleted("Trail_Marker");
        bool marketDone = TaskManager.Instance.IsCompleted("Market_Supplies");
        bool curioDone = ProgressionManager.Instance.act4CurioEncounterPlayed;

        dirtyMarkerObject.SetActive(!markerDone);
        cleanMarkerObject.SetActive(markerDone);
        curioEncounterTrigger.SetActive(markerDone && marketDone && !curioDone);
        act4Curio.SetActive(markerDone && marketDone && !curioDone);
    }

    public void CompleteOrchardCare()
    {
        if (TaskManager.Instance.IsCompleted("Orchard_Care"))
            return;

        TaskManager.Instance.CompleteTask("Orchard_Care");
        ApplySavedWorldState();
    }

    public void CompletePlantHope()
    {
        if (TaskManager.Instance.IsCompleted("Plant_Hope"))
            return;

        TaskManager.Instance.CompleteTask("Plant_Hope");
        ApplySavedWorldState();
    }

    public void MarkBenchVisionSeen()
    {
        if (ProgressionManager.Instance.act3BenchVisionSeen)
            return;

        ProgressionManager.Instance.act3BenchVisionSeen = true;
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    public void MarkNewspaperFound()
    {
        if (ProgressionManager.Instance.act3NewspaperFound)
            return;

        ProgressionManager.Instance.act3NewspaperFound = true;
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }

    public void CompleteTrailMarker()
    {
        if (TaskManager.Instance.IsCompleted("Trail_Marker"))
            return;

        TaskManager.Instance.CompleteTask("Trail_Marker");
        ApplySavedWorldState();
    }

    public void MarkCurioEncounterPlayed()
    {
        if (ProgressionManager.Instance.act4CurioEncounterPlayed)
            return;

        ProgressionManager.Instance.act4CurioEncounterPlayed = true;
        ProgressionManager.Instance.SaveProgress();
        ApplySavedWorldState();
    }
}