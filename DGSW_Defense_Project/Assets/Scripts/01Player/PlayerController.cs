using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Player_Status p_status;
    Enemy_Status e_status;
    public CharacterController player;

    float x;
    float z;
    bool jump;

    //public float speed = 0.0f;
    public float maxSpeed = 12.0f;
    public float jumpHeight = 3.0f;
    public float gravity = -9.81f;


    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    private float cur_hp;

    Vector3 velocity;

    bool isGrounded;
    int isMoving;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        p_status = FindObjectOfType<Player_Status>();
        e_status = FindObjectOfType<Enemy_Status>();
        cur_hp = p_status.defalt_Health;
    }

    void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        jump = Input.GetButtonDown("Jump");
    }

    void Update()
    {
        Move();
        Jump();
        GetSpeed();
        MoveCheck();
        GroundCheck();
        PlayAnimation();
        Death();
    }

    void Move()
    {

        Vector3 move = transform.right * x + transform.forward * z;
        player.Move(move * p_status.defalt_Speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (jump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }

    void MoveCheck()
    {
        if (x != 0 || z != 0)
        {
            isMoving = 1;
        }
        else
        {
            isMoving = 0;
        }
    }
    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }
    }

    void GetSpeed()
    {
        p_status.defalt_Speed = Mathf.Lerp(0.0f, maxSpeed, isMoving * 0.3f);
        //Debug.Log("[PlayreController]GetSpeed / speed :"+ p_status.defalt_Speed);
    }

    void PlayAnimation()
    {
        animator.SetFloat("Speed_f", p_status.defalt_Speed);
    }

    void Death()
    {
        if (cur_hp < 0)
            animator.SetBool("Death_b",true);
    }

    public void HitByExplosion(Vector3 explosionPos)
    {
        cur_hp -= e_status.explosion_Damage;
        Debug.Log("Explosion_Enemy_atk : " + cur_hp);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("[PlayreController]OntriggerEnter");
        //Debug.Log("[PlayreController]OnTriggerEnter/other : " + other);

        if (other.tag == "Enemy_atk")
        {

            Debug.Log("[PlayreController]OntriggerEnter/e.status.defalt_Damage : " + e_status.defalt_Damage);
            cur_hp -= e_status.defalt_Damage;

            Debug.Log("Enemy_atk : " + cur_hp);
        }

        if(other.tag == "Aerial_atk")
        {
            Debug.Log("[PlayreController]OntriggerEnter/e.status.aerial_Damage : " + e_status.aerial_Damage);
            cur_hp -= e_status.aerial_Damage;

            Debug.Log("Aerial_atk : " + cur_hp);
        }
    }
}
