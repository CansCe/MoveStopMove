using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager _instance;
    public static GameStateManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameStateManager();
            }
            return _instance;
        }
    }
    public IState CurrentGameState { get; private set; }
    public delegate void GameStateChangeHandler(IState newGameState);
    public event GameStateChangeHandler OnGameStateChanged;
    private GameStateManager()
    {

    }
    public void SetState(IState newGameState)
    {
        if (newGameState == CurrentGameState)
            return;
        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(newGameState);
    }
}
