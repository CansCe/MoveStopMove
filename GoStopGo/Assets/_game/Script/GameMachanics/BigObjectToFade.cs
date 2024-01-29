using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToFade : MonoBehaviour
{
    public Material baseShader;
    public Material fadeShader;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<Renderer>().material = fadeShader;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<Renderer>().material = baseShader;
        }
    }
}
