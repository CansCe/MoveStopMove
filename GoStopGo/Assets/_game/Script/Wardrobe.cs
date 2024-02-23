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

    void Start()
    {
        instance = this;        
    }
    public GameObject Get_Hats(int index)
    {
        return hats[index];
    }
    public GameObject Get_Weapons(int index)
    {
        return weapons[index];
    }
}
