using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public Text gold_text;

    private void Start()
    {
        instance = this;
        Update_Shop();
    }

    public void Purchase_Weapon()
    {
        if(GameManager.instance.player_Gold >= 100)
        {
            GameManager.instance.player_Gold -= 100;
            GameManager.instance.obtained_weapon[0] = 1;
            GameManager.instance.SaveGame(0);
        }
    }

    public void Update_Shop()
    {
        gold_text.text = GameManager.instance.player_Gold.ToString();
    }
}
