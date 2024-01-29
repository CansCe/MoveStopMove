using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int player_Gold;
    public static GameManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        instance = this;
        //load player_Gold
        player_Gold = PlayerPrefs.GetInt("gold");
        Debug.Log(player_Gold);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveGame(int gold)
    {
        //save game
        player_Gold += gold;
        PlayerPrefs.SetInt("gold", player_Gold);
        PlayerPrefs.Save();
        if(player_Gold > 500)
            PlayerPrefs.DeleteAll();
    }
    public void StartLevel()
    {
        foreach (GameObject bot in SimplePool.instance.list_Pooled_Bots)
        {
            bot.SetActive(true);
        }
        //get to UImanager to disable start UI and enable player control ui
        UIManager.instance.StartLevel();
    }

    public void PauseGame()
    {

    }
}
