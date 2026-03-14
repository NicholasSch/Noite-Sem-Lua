using UnityEngine;

public class NewspaperInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Act3FarmManager act3FarmManager;
    [SerializeField] private NewspaperUI newspaperPrefab;

    public void Interact()
    {
        if (ProgressionManager.Instance.act3NewspaperFound)
            return;

        if (FindFirstObjectByType<NewspaperUI>() != null)
            return;

        GameStateManager.SetState(GameState.Cutscene);

        NewspaperUI newspaperInstance = Instantiate(newspaperPrefab);
        newspaperInstance.Setup(OnNewspaperClosed);
    }

    private void OnNewspaperClosed()
    {
        act3FarmManager.MarkNewspaperFound();
        GameStateManager.SetState(GameState.Gameplay);
    }
}