using System.Collections;
using UnityEngine;

public class TreeInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private FarmDay3Manager farmDay3Manager;
    [SerializeField] private AudioClip wateringSound;

    public void Interact()
    {
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        AudioManager.Instance.PlaySFX(wateringSound);


        string[] lines =
        {
            "<color=#531182>Lucas:</color> Isso é impossível...",
            "Eu plantei essa muda ontem.",
            "Agora ela já passa da minha cintura.",
            "É como se a terra tivesse empurrado a árvore para fora durante a noite.",
            "O vovô tinha razão.",
            "O tempo aqui corre de um jeito diferente."
        };

        yield return ThoughtUI.Instance.PlaySequence(lines);

        farmDay3Manager.CompleteSentinelTask();
        gameObject.SetActive(false);
    }
}