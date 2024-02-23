using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Character : MonoBehaviour
{
    public int hp = 1;
    public float speed = 15;
    public bool canMove = true;
    public bool isDead => hp<=0;
    public int current_bullet = 1;
    public float detect_radius = 1.5f;
    public float indicator_radius = 3.0f;

    public float range;
    public Rigidbody rb;
    public Animator anim;
    public int gold_Earned =0;
    string current_Animation = "Idle";

    public Vector3 target_position;
    public Transform target_Transform;

    public GameObject head;

    public static Character _instance;

    private void Awake()
    {
        _instance = GetComponent<Character>();
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }
    protected virtual void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }
    public void ChangeAnim(string newAnimation)
    {
        if(current_Animation != "")
        {
            anim.ResetTrigger(current_Animation);
        }
        current_Animation = newAnimation;
        anim.SetTrigger(newAnimation);
    }
    public virtual void Dead()
    {
        ChangeAnim("Dead");
        canMove = false;
        ParticleSystem temparory = SimplePool.instance.Get_Pooled_Particle();
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        if(temparory == null)
        {
            return;
        }
        temparory.transform.position = position;
        temparory.gameObject.SetActive(true);
        temparory.transform.rotation = Quaternion.Euler(0, 0, 0);
        temparory.Play();
        rb.velocity = Vector3.zero;
        transform.position = transform.position;
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
            GameObject clone = SimplePool.instance.Get_Pooled_Bullet();
            clone.SetActive(true);
            clone.transform.position = transform.position + transform.forward * 2f;
            clone.gameObject.GetComponent<Bullet>().target_position= target_position;
            clone.gameObject.GetComponent<Bullet>().parent = transform;
            canMove = true;
        }
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
    public void LevelUp()
    {
        transform.localScale = new Vector3(transform.localScale.x + 0.15f, transform.localScale.y + 0.15f, transform.localScale.z + 0.15f);
        transform.position = transform.position + new Vector3(0, 0.15f, 0);
        if (gameObject.CompareTag("Player"))
        {
            GetComponent<Player>().self_Capsule_HitBox.transform.localScale = new Vector3(GetComponent<Player>().self_Capsule_HitBox.transform.localScale.x + 0.15f, GetComponent<Player>().self_Capsule_HitBox.transform.localScale.y + 0.15f, GetComponent<Player>().self_Capsule_HitBox.transform.localScale.z + 0.15f);
            GetComponent<Player>().detect_Range.transform.localScale = new Vector3(GetComponent<Player>().detect_Range.transform.localScale.x + 0.15f, GetComponent<Player>().detect_Range.transform.localScale.y + 0.15f, GetComponent<Player>().detect_Range.transform.localScale.z + 0.15f);
            _Camera.instance.adding_Vector = new Vector3(_Camera.instance.adding_Vector.x, _Camera.instance.adding_Vector.y + 1.5f, _Camera.instance.adding_Vector.z-0.5f );
            range += 1.5f;
        }
    }
    public virtual void WearHat(int index)
    {
        Instantiate(Wardrobe.instance.Get_Hats(index), head.transform.position, Quaternion.identity,head.transform);
    }
    
    public void PauseAnim()
    {
        if (anim == null)
        {
            return;
        }
        anim.speed = 0;
    }
    public void ResumeAnim()
    {
        if (anim == null)
        {
            return;
        }
        anim.speed = 1;
    }
    protected virtual void OnGameStateChanged(GameManager.GameState newGameState)
    {
        enabled = newGameState == GameManager.GameState.Paused;
        if (!enabled)
        {
            PauseAnim();
        }
        else
        {
            ResumeAnim();
        }
    }
}
