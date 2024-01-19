using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Player : Character
{
    public Joystick control_joystick;
    public static Player instance;
    public GameObject self_Capsule_HitBox;
    public int current_Targeted_Enemy=0;
    void Start()
    {
        instance = this;
        control_joystick = FindAnyObjectByType<Joystick>();
    }
    void Update()
    {
        if(isDead != true)
            if(canMove)
                Rotate_And_AnimChange();
        if(Vector3.Distance(transform.position,target_position) > range)
        {
            current_Targeted_Enemy = 0;
            target_position = Get_Nearest_Enemy();
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
        rb.velocity = new Vector3(control_joystick.Horizontal * speed, 0, control_joystick.Vertical * speed);
    }
    public void Rotate_And_AnimChange()
    {
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
                    ChangeAnim("Idle");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bot"))
        {
            target_position = other.gameObject.transform.position;
            target_Transform = other.gameObject.transform;
            if(current_Targeted_Enemy == 0)
            {
                current_Targeted_Enemy++;
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bot"))
        {
            if (other.gameObject.GetComponent<Character>().isDead)
            {
                target_position = Vector3.zero;
                target_Transform = null;
                transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                self_Capsule_HitBox.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.005f, transform.position.z);
                _Camera.instance.adding_Vector.y += 0.015f;
                _Camera.instance.adding_Vector.z -= 0.015f;
            }
        }
    }
}
