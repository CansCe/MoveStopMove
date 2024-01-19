using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public Transform parent;
    public float speed;
    public Vector3 target_position;
    public float existenceTime = 2f;
    bool goback = false;
    void Update()
    {
        transform.Rotate(0, 10, 0);
        if (goback)
        {
            MoveBack();
        }
        else
        {
            MoveTowardEnemy();
        }
        if(Vector3.Distance(transform.position, target_position) < 0.1f)
        {
            goback = true;
        }
        if(Vector3.Distance(transform.position, parent.position) < 0.1f )
        {
            if(goback == true)
            gameObject.SetActive(false);
        }
        existenceTime -= Time.deltaTime;
        if (existenceTime <= 0.1f)
        {
            gameObject.SetActive(false);
            existenceTime = 2f;
        }
    }
    void MoveTowardEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, target_position, speed * Time.deltaTime);
    }
    void MoveBack()
    {
        transform.position = Vector3.MoveTowards(transform.position, parent.position, speed * Time.deltaTime);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Bullet"))
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}
}
