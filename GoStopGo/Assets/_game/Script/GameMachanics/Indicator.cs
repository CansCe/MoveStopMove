using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public GameObject player;
    public Canvas showing_canvas;
    public static Indicator instance;

    private void Start()
    {
        instance = this;
    }


}
