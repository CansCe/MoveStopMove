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

    public GameObject _player;
    public static GameManager instance;

    public static event Action<GameState> OnGameStateChanged;

    public GameState current_State;
    
    //this canvas.text to display player gold on screen
    public UnityEngine.UI.Text gold_Text;
    void Start()
    {
        instance = this;
        LoadGame();
    }   
    void Update()
    {
        
    }
    public void SaveGame(int gold)
    {
        player_Gold += gold;

        PlayerPrefs.SetInt("gold", player_Gold);
        PlayerPrefs.Save();
        if(player_Gold >= 1000)
        {
            player_Gold = 0;
            SaveGame(0);
        }
    }
    public void LoadGame()
    {
        player_Gold = PlayerPrefs.GetInt("gold");
        Debug.Log(player_Gold);
    }
    public void StartLevel()
    {
        GameObject player =Instantiate(_player, new Vector3(0, 1, 0), Quaternion.identity);
        SimplePool.instance.Level_Start();
        UIManager.instance.StartLevel();
        player.SetActive(true);
    }
    public void ClearLevel()
    {
        foreach (GameObject bot in SimplePool.instance.list_Pooled_Bots)
        {
            bot.SetActive(false);
        }
    }
    public void Restart()
    {
        UIManager.instance.StartLevel();
        Player.instance.playAgain();
        foreach(GameObject bot in SimplePool.instance.list_Pooled_Bots)
        {
            bot.GetComponent<Bot>().hp = 1;
            bot.SetActive(true);
        }
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
