using UnityEngine;

public enum GameState
{
    Gameplay,
    Dialogue,
    Letter,
    Journal,
    Cutscene
}
public class GameStateManager : MonoBehaviour
{
public static bool LetterWasRead;
public static GameState CurrentState = GameState.Gameplay;

}
