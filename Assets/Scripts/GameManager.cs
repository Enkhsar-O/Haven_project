using UnityEngine;
using System.Collections;

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver,
    Story
}
public class GameManager : MonoBehaviour
{

    [Header("Game State")]
    public static GameManager instance;

    public GameState currentGameState;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentGameState = GameState.Playing;
    }
    public GameState GetCurrentGameState()
    {

        return currentGameState;
    }
    public void SetCurrentGameState(GameState newGameState)
    {

        Debug.Log("Current Game State: " + currentGameState);
        currentGameState = newGameState;
        Debug.Log("New Game state: " + currentGameState);
    }
}
