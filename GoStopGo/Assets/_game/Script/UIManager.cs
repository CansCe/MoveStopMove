using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas start_Game_Canvas;
    public Canvas player_contoller_Canvas;
    public Canvas shop_Canvas;

    public static UIManager instance;
    private void Start()
    {
        instance = this;
    }

    public void StartLevel()
    {
        start_Game_Canvas.gameObject.SetActive(false);
        player_contoller_Canvas.gameObject.SetActive(true);
    }
    public void ResetLevel()
    {
        start_Game_Canvas.gameObject.SetActive(true);
        player_contoller_Canvas.gameObject.SetActive(false);
    }
    public void OpenShop()
    {
        shop_Canvas.gameObject.SetActive(true);
        start_Game_Canvas.gameObject.SetActive(false);
    }
}
