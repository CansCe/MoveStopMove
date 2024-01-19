using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public float wanderRadius;
    public float wanderTimer;
    public float waitToShootTime;
    public NavMeshAgent agent ;
    public Transform target_Player;
    public Vector3 next_Position;
    public GameObject is_Targeted;
    float timer;
 
    void  Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }
    void Update()
    {
        if(isDead != true)
            if(canMove)
            Move_And_ChangeAnim();
        target_position = Get_Nearest_Enemy();
    }
    void Move_And_ChangeAnim()
    {
        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            next_Position = newPos;
            agent.SetDestination(newPos);
            timer = 0;
        }
        if (agent.velocity != Vector3.zero)
        {
            ChangeAnim("Run");
        }
        else
        {
            ChangeAnim("Idle");
        }
    }
    Vector3 RandomNavSphere(Vector3 origin,float dist , int layermask)
    {
        Vector3 random_Direction = Random.insideUnitSphere* dist;
        random_Direction += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(random_Direction,out navHit, dist, layermask);   
        return navHit.position;
    }
    public override IEnumerator Shoot(Vector3 target_position)
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
            transform.LookAt(target_position);
            agent.ResetPath();
            yield return new WaitForSeconds(0.35f);
            GameObject clone = SimplePool.instance.Get_Pooled_Bullet();
            clone.SetActive(true);
            clone.transform.position = transform.position + transform.forward * 1.5f;
            clone.gameObject.GetComponent<Bullet>().target = target_position;
            canMove = true;
        }
     }
    public override Vector3 Get_Nearest_Enemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        float min = Mathf.Infinity;
        Vector3 temparory = Vector3.zero;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Bot") || collider.gameObject.CompareTag("Player"))
            {
                float distance = Vector3.Distance(transform.position, collider.gameObject.transform.position);
                if (distance < min)
                {
                    min = distance;
                    temparory = collider.gameObject.transform.position;
                    target_Transform = collider.gameObject.transform;
                }
            }
        }
        return temparory;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target_Player = other.gameObject.transform;
            if (current_bullet > 0)
            {
                StartCoroutine(Shoot(target_Player.position));
                current_bullet--;
            }
            else
            {
                agent.SetDestination(RandomNavSphere(transform.position, wanderRadius , -1));
                current_bullet++;
            }
        }
        if (other.gameObject.CompareTag("Bot"))
        {
            target_position = other.gameObject.transform.position;
            target_Transform = other.gameObject.transform;
            if (current_bullet > 0)
            {
                StartCoroutine(Shoot(target_position));
                current_bullet--;
            }
            else
            {
                agent.SetDestination(RandomNavSphere(transform.position, wanderRadius , -1));
                current_bullet++;
            }
        }
    }
    public override void Dead()
    {
        base.Dead();
        is_Targeted.SetActive(false);
        StartCoroutine(After_Dead());
    }
    IEnumerator After_Dead()
    {
        yield return new WaitForSeconds(2.05f);
        gameObject.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target_Player = null;
            is_Targeted.SetActive(false);
        }
    }
}