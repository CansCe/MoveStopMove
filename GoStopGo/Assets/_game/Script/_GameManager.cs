using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int gold_got;
    public static GameManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        instance = this;
        //load gold_got
        gold_got = PlayerPrefs.GetInt("gold");
        Debug.Log(gold_got);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveGame(int gold)
    {
        //save game
        gold_got += gold;
        PlayerPrefs.SetInt("gold", gold_got);
        PlayerPrefs.Save();
        if(gold_got > 500)
            PlayerPrefs.DeleteAll();
    }
}
