using UnityEngine;

public class NewspaperCucaInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay2Manager farmDay2Manager;
    [SerializeField] private NewspaperUI newspaperPrefab;

    public void Interact()
    {
        if (FindFirstObjectByType<NewspaperUI>() != null)
            return;

        GameStateManager.SetState(GameState.Cutscene);

        NewspaperUI newspaperInstance = Instantiate(newspaperPrefab);
        newspaperInstance.Setup(OnNewspaperClosed);
    }

    private void OnNewspaperClosed()
    {   
        GameStateManager.SetState(GameState.Gameplay);

        farmDay2Manager.MarkNewspaperFound();
    }
}