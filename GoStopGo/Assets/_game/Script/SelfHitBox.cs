using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfHitBox : MonoBehaviour
{
    public GameObject parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (parent.GetComponent<Character>().hp > 0)
            {
                //trigger the parent's hit function
                parent.GetComponent<Character>().Dead();
            }
        }
        if (other.gameObject.CompareTag("Boomerang"))
        {
            return;
        }
    }
}
