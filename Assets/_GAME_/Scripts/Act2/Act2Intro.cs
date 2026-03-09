using System.Collections;
using UnityEngine;

public class SceneIntro : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        string[] lines =
        {
            "<color=#531182>Lucas:</color> Droga... o motor ferveu bem na entrada.",
            "Pelo menos eu já trouxe o caderno do vovô.",
            "Parece que vou ter tempo de sobra para ler enquanto esse ferro velho esfria."
        };

        yield return ThoughtUI.Instance.PlaySequence(lines);

        GameStateManager.CurrentState = GameState.Gameplay;
    }
}