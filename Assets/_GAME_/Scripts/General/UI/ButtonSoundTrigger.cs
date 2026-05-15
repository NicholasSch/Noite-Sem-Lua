using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonSoundTrigger : MonoBehaviour, IPointerEnterHandler
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip hoverSound;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(PlayClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable && hoverSound != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayUI(hoverSound);
        }
    }

    public void PlayClick()
    {
        if (button.interactable && clickSound != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayUI(clickSound);
        }
    }
}