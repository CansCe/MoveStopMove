using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Player : Character
{
    public int current_hat;
    public GameObject detect_Range;
    public Joystick control_joystick;
    public int current_Targeted_Enemy=0;
    public GameObject self_Capsule_HitBox;
    public static Player instance;
    public bool destroyHat;

    private bool trigger = false;

    void Start()
    {
        instance = this;
        
        WearHat(2);
    }
    void Update()
    {
        if (isDead)
        {
            if (!trigger)
            {
                Dead();
                trigger = true;
            }
            return;
        }
        if (isDead != true)
            if (canMove)
            {
                Rotate_And_AnimChange();
                if (Vector3.Distance(transform.position, target_position) > range)
                {
                    current_Targeted_Enemy = 0;
                    target_position = Get_Nearest_Enemy();
                }
            }
        if (destroyHat)
        {
            ClearHat();
        }
    }
    void FixedUpdate()
    {
        if(isDead != true )
            if(canMove)
                Move();
    }
    public void Move()
    {
        if(control_joystick==null)
            control_joystick = FindAnyObjectByType<Joystick>();
        else
            rb.velocity = new Vector3(control_joystick.Horizontal * speed, 0, control_joystick.Vertical * speed);
    }
    public void Rotate_And_AnimChange()
    {
        if (control_joystick == null)
        {
            control_joystick = FindAnyObjectByType<Joystick>();
            return;
        }
        Vector3 input = new Vector3(control_joystick.Horizontal, 0, control_joystick.Vertical);
        if(input != Vector3.zero)
        {
            ChangeAnim("Run");
            transform.rotation = Quaternion.LookRotation(input);
            if (current_bullet == 0) 
                current_bullet++;
        }
        else
        {
            if(target_position != Vector3.zero && current_bullet > 0)
            {
                target_position = Get_Nearest_Enemy();
                StartCoroutine(Shoot(target_position));
                current_bullet--;
            }
            else
            {
                if(canMove || !isDead)
                    if(input == Vector3.zero)
                        ChangeAnim("Idle");
            }
        }
    }
    public override void Dead()
    {
        base.Dead();
        if(gold_Earned >0)
            GameManager.instance.SaveGame(gold_Earned);
    }

    public void Paused()
    {
        canMove = false;
        rb.velocity = Vector3.zero;
        ChangeAnim("Idle");
    }
    public void ClearHat()
    {
        foreach (Transform child in head.transform)
        {
            if (child.gameObject.name == Wardrobe.instance.Get_Hats(current_hat).name + "(Clone)")
            {
                Destroy(child.gameObject);
            }
            else
                continue;
        }
    }
}
