using UnityEngine;

public class NewspaperUI : MonoBehaviour
{
    private System.Action onClosed;

    public void Setup(System.Action closeCallback)
    {
        onClosed = closeCallback;
    }

    public void Close()
    {
        onClosed?.Invoke();
        Destroy(gameObject);
    }
}