using UnityEngine;
public enum GameState
{
    Menu,
    Gameplay, 
    Paused,  
    Narration,
    Thought,
    Journal,
    Letter,
    Cutscene
}

public class GameStateManager : MonoBehaviour
{
    public static GameState CurrentState { get; private set; } = GameState.Menu;

    public static void SetState(GameState newState)
    {
        CurrentState = newState;
    }
}