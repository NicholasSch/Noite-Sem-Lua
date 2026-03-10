using UnityEngine;

public enum GameState
{
    Gameplay,
    Narration,
    Thought,
    Journal,
    Letter,
    Cutscene
}
public class GameStateManager : MonoBehaviour
{
public static GameState CurrentState = GameState.Gameplay;

}
