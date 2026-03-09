using UnityEngine;

public enum GameState
{
    Gameplay,
    Dialogue,
    Letter,
    Cutscene
}
public class GameStateManager : MonoBehaviour
{
public static bool LetterWasRead;
public static GameState CurrentState = GameState.Gameplay;

}
