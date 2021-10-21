using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public CharacterController player;

    float x;
    float z;
    bool jump;

    public float speed = 0.0f;
    public float maxSpeed = 12.0f;
    public float jumpHeight = 3.0f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    int isMoving;

    public Animator animator;

	void Start()
	{
        animator = GetComponent<Animator>();	
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
    }

    void Move(){

        Vector3 move = transform.right * x + transform.forward * z;
        player.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity * Time.deltaTime);
    }

    void Jump() {
        if(jump){
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
		}
	}

    void MoveCheck() {
            if(x != 0 || z != 0){
                isMoving = 1;
			}
            else{
                isMoving = 0;
			}
	}
    void GroundCheck() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2.0f;
		}
	}

    void GetSpeed() {
        speed = Mathf.Lerp(0.0f, maxSpeed, isMoving * 0.3f);
    }

    void PlayAnimation() {
            animator.SetFloat("Speed_f", speed);
    }
}
