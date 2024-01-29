using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    public static Wardrobe instance;
    public GameObject[] hats;
    public GameObject[] weapons;
    public Material[] pant;
    public Material[] sets;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;        
    }
    public GameObject get_Hats(int index)
    {
        return hats[index];
    }
    public GameObject get_Weapons(int index)
    {
        return weapons[index];
    }
}
