using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Camera : MonoBehaviour
{
    public GameObject player;
    public Vector3 adding_Vector;
    public static _Camera instance;
    void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            transform.position = player.transform.position + adding_Vector;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
