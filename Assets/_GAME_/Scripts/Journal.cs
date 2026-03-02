using System;
using UnityEngine;

public class JournalInteractable : MonoBehaviour, IInteractable
{
    public GameObject JournalUI;

    public void Awake()
    {
        Close();
    }

    public void Interact()
    {
        JournalUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Close()
    {
    JournalUI.SetActive(false);
    Time.timeScale = 1f;
    }
}