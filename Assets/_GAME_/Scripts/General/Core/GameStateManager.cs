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
    public static GameState CurrentState { get; private set; } = GameState.Gameplay;

    public static void SetState(GameState newState)
    {
        CurrentState = newState;
    }
}