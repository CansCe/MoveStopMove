using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Vector3 target;
    public float existenceTime = 2f;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.Rotate(0, 0, 10);
        existenceTime -= Time.deltaTime;
        if (existenceTime <= 0.1f)
        {
            gameObject.SetActive(false);
            existenceTime = 2f;
        }
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }
}