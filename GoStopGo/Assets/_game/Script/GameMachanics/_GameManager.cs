using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int player_Gold;
    public int[] obtained_weapon;
    //obtained weapon string
    public int[] obtained_pants;
    public int[] obtained_sets;

    public static GameManager instance;

    public static event Action<GameState> OnGameStateChanged;

    public GameState current_State;
    
    void Start()
    {
        instance = this;
        LoadGame();
        StartLevel();
    }   
    void Update()
    {
        
    }
    public void SaveGame(int gold)
    {
        player_Gold += gold;

        PlayerPrefs.SetInt("gold", player_Gold);
        PlayerPrefs.Save();
    }
    public void LoadGame()
    {
        player_Gold = PlayerPrefs.GetInt("gold");
        Debug.Log(player_Gold);
    }
    public void StartLevel()
    {
        SimplePool.instance.Level_Start();
        UIManager.instance.StartLevel();
    }
    public void ResetLevel()
    {
        foreach (GameObject bot in SimplePool.instance.list_Pooled_Bots)
        {
            bot.SetActive(false);
        }
        UIManager.instance.ResetLevel();
    }
    public void PauseGame()
    {
        SimplePool.instance.Pause_State();
    }
    public void ResumeLevel()
    {
        SimplePool.instance.Resume_State();
    }
    public void UpdateGameState(GameState newState)
    {
        current_State = newState;

        switch (newState)
        {
            case GameState.Start:
                break;
            case GameState.Play:
                StartLevel();
                break;
            case GameState.Paused:
                PauseGame();
                break;
            case GameState.GameOver:
                break;
            case GameState.Resume:
                ResumeLevel();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }
    }
    public enum GameState
    {
        Start,
        Play,
        Paused,
        Resume,
        GameOver,
    }
}
