using System.Collections;
using UnityEngine;

public class Act6ProtectionInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay3Manager farmDay3Manager;
    [SerializeField] private AudioClip fireSound;

    public void Interact()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        if (fireSound != null)
        {
            AudioManager.Instance.PlaySFX(fireSound);
        }

        string[] lines =
        {
            "<color=#531182>Lucas:</color> Arruda... guiné...",
            "O caderno manda deixar isso queimando na fogueira.",
            "\"Para manter o redemoinho longe.\"",
            "Quanto mais eu leio o que o vovô escreveu, menos isso parece superstição.",
            "Se alguma coisa entrar aqui hoje, eu quero a casa pronta."
        };

        yield return ThoughtUI.Instance.PlaySequence(lines);

        farmDay3Manager.CompleteHouseWhistleTask();
        gameObject.SetActive(false);
    }
}