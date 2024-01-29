using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Vector3 target_position;
    public float existenceTime = 2f;
    public Transform parent;
    // Update is called once per frame
    public virtual void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target_position, speed * Time.deltaTime);
        transform.Rotate(0, 0, 10);
        existenceTime -= Time.deltaTime;
        if (existenceTime <= 0.1f)
        {
            gameObject.SetActive(false);
            existenceTime = 2f;
        }
        if (Vector3.Distance(transform.position, target_position) < 0.1f)
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
        if (other.gameObject.CompareTag("HitBox"))
        {
            gameObject.SetActive(false);
            if (parent.CompareTag("Player"))
            {
                parent.GetComponent<Character>().gold_Earned += 50;
                parent.GetComponent<Character>().LevelUp();
            }else if(parent.CompareTag("Bot"))
            {
                parent.GetComponent<Character>().LevelUp();
            }
        }
    }
}