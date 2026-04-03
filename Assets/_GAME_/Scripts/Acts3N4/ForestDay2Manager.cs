using UnityEngine;

public class ForestDay2Manager : MonoBehaviour
{
    void Start()
    {
        GameUI.Instance.gameObject.SetActive(!ProgressionManager.Instance.act4HideGameUI);
    }
}