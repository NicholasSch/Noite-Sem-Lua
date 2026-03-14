using UnityEngine;

public class Act3FarmManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip dayFarmMusic;
    [SerializeField] private AudioClip dayFarmAmbience;

    [Header("Objects")]
    [SerializeField] private GameObject vegetationGrid;
    [SerializeField] private GameObject vegetationGrid2;
    [SerializeField] private GameObject saplingInteractable;
    [SerializeField] private GameObject newspaperInteractable;
    [SerializeField] private GameObject plantedTreeObject;

    private void Start()
    {
        AudioManager.Instance.PlayAmbient(dayFarmAmbience);
        AudioManager.Instance.PlayMusic(dayFarmMusic);
        
        ApplySavedWorldState();
    }

    public void ApplySavedWorldState()
    {
        bool orchardDone = TaskManager.Instance.IsCompleted("Orchard_Care");
        bool benchVisionSeen = ProgressionManager.Instance.act3BenchVisionSeen;
        bool plantDone = TaskManager.Instance.IsCompleted("Plant_Hope");
        bool newspaperFound = ProgressionManager.Instance.act3NewspaperFound;

        vegetationGrid.SetActive(!orchardDone);
        vegetationGrid2.SetActive(orchardDone);
        saplingInteractable.SetActive(benchVisionSeen && !plantDone);
        newspaperInteractable.SetActive(!newspaperFound);
        plantedTreeObject.SetActive(plantDone);
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
}