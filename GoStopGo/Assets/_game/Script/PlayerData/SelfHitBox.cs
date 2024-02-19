using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfHitBox : MonoBehaviour
{
    public GameObject parent;
    public GameObject indicator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (parent.GetComponent<Character>().hp > 0)
            {
                parent.GetComponent<Character>().hp -= 1;
            }
        }
    }
}
