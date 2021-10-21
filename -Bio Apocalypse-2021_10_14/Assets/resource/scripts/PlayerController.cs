using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public PlayerBlueprint playerBlueprint;

	public CharacterController player;

	float x;
	float z;
	bool jump;

	public float speed = 0.0f;
	public float jumpHeight = 3.0f;
	public float gravity = -9.81f;

	public Transform groundCheck;
	public float groundDistance = 0.1f;
	public LayerMask groundMask;

	Vector3 velocity;

	bool isGrounded;
	int isMoving;

	public Animator animator;

	private GameObject bullet;

	public GunBlueprint gunBlueprint;
	public GameObject firePoint;
	public Transform aim;

	bool ifClick = false;
	bool ifFireRate = false;
	bool reload = false;

	float timer = 0.0f;

	int magazine;

	void Awake()
	{
		aim.localPosition = new Vector3(0.0f, 0.0f, gunBlueprint.fireRange);
	}

	void Start()
	{
		Setup();
	}

	void FixedUpdate()
	{
		x = Input.GetAxis("Horizontal");
		z = Input.GetAxis("Vertical");
		jump = Input.GetButtonDown("Jump");
		ifClick = Input.GetButton("Fire1");
	}

	void Update()
	{
		Move();
		Jump();
		GetSpeed();
		MoveCheck();
		GroundCheck();
		PlayAnimation();
		Fire();
		FireRateCountdown();
	}

	void Move()
	{
		animator.SetFloat("Body_Horizontal_f", x);
		animator.SetFloat("Body_Vertical_f", z);
		
		Vector3 move = transform.right * x + transform.forward * z;
		player.Move(move * speed * Time.deltaTime);

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
		speed = Mathf.Lerp(0.0f, playerBlueprint.speed, isMoving * 0.3f);
	}

	void PlayAnimation()
	{
		animator.SetFloat("Speed_f", speed);
	}

	void Fire()
	{
		animator.SetBool("Shoot_b", ifClick);
		Debug.Log(firePoint.transform.position);
		if (ifClick && !ifFireRate)
		{
			bullet = Instantiate(gunBlueprint.bullet, firePoint.transform.position, firePoint.transform.rotation);
			bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * gunBlueprint.bulletSpeed);
			Destroy(bullet, 2.0f);

			magazine--;
			if (magazine == 0)
			{
				reload = true;
			}
			else
			{
				timer = gunBlueprint.fireRate;
				ifFireRate = true;
			}
		}
	}

	void FireRateCountdown()
	{
		if (timer >= 0)
		{
			timer -= Time.deltaTime;
		}
		else
		{
			ifFireRate = false;
		}
	}

	void Setup()
	{
		animator = GetComponentInChildren<Animator>();
		animator.SetInteger("WeaponType_int", gunBlueprint.gunType);
        magazine = gunBlueprint.magazine;

	}
}
