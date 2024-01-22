using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Character : MonoBehaviour
{
    public Animator anim;
    public int hp = 1;
    public float speed = 15;
    public float radius = 1.5f;
    public bool isDead = false;
    public bool canMove = true;
    public float range;
    public Rigidbody rb;
    public Vector3 target_position;
    public int current_bullet = 1;
    public Transform target_Transform;

    string current_Animation = "Idle";

    public void ChangeAnim(string newAnimation)
    {
        if(current_Animation != "")
        {
            anim.ResetTrigger(current_Animation);
        }
        current_Animation = newAnimation;
        anim.SetTrigger(newAnimation);
    }

    public virtual IEnumerator Shoot(Vector3 target_position)
    {
        if(target_position == Vector3.zero)
        {
            yield return null;
        }
        else
        {
            canMove = false;
            rb.velocity = Vector3.zero;
            ChangeAnim("Attack");
            target_position = Get_Nearest_Enemy();
            target_position.y = transform.position.y;
            transform.LookAt(target_position);
            yield return new WaitForSeconds(0.39f);
            GameObject clone = SimplePool.instance.Get_Pooled_Boomerang();
            clone.SetActive(true);
            clone.transform.position = transform.position + transform.forward * 1.5f;
            clone.gameObject.GetComponent<Boomerang>().target_position= target_position;
            clone.gameObject.GetComponent<Boomerang>().parent = transform;
            canMove = true;
        }
    }
    public virtual void Dead()
    {
        if(isDead == true)
        {
            return;
        }
        ChangeAnim("Dead");
        ParticleSystem temparory =SimplePool.instance.Get_Pooled_Particle();
        Vector3 position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
        temparory.transform.position = position;
        temparory.gameObject.SetActive(true);
        temparory.transform.rotation = Quaternion.Euler(0, 0, 0);
        temparory.Play();
        rb.velocity = Vector3.zero;
        transform.position = transform.position;
        isDead = true;
    }
    public virtual Vector3 Get_Nearest_Enemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        GameObject clone ;
        float min = Mathf.Infinity;
        Vector3 temparory = Vector3.zero;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Bot"))
            {
                float distance = Vector3.Distance(transform.position, collider.gameObject.transform.position);
                if (distance < min)
                {
                    if(collider.gameObject.GetComponent<Character>().isDead)
                    {
                        continue;
                    }
                    min = distance;
                    temparory = collider.gameObject.transform.position;
                    target_Transform = collider.gameObject.transform;
                    clone = collider.gameObject;
                    if (clone != null)
                        clone.gameObject.GetComponent<Bot>().is_Targeted.SetActive(true);
                }
            }
        }
        if(temparory != Vector3.zero)
        {
            
            return temparory;
        }
        else
        {
            target_Transform = null;
            return Vector3.zero;
        }
    }
}
