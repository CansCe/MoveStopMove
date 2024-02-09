using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    private void Start()
    {
        instance = this;
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
}
