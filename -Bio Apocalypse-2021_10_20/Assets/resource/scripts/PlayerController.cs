using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerController : MonoBehaviour
{

	public PlayerBlueprint playerBlueprint;

	public CharacterController player;

	float h;
	float v;
	bool jump;

	public float speed = 0.0f;
	public float jumpHeight = 3.0f;
	public float gravity = -9.81f;

	public Transform groundCheck;
	public float groundDistance = 0.1f;
	public LayerMask groundMask;

	Vector3 velocity;

	bool isJumping;
	int isMoving;

	public Animator animator;

	private GameObject bullet;

	public GunBlueprint gunBlueprint;
	public GameObject firePoint;
	public Transform aim;

	bool isClick = false;
	bool isFireRate = false;
	bool isReload = false;

	float timer = 0.0f;

	int magazine;
	int hp;


	void Start()
	{
		// 캐릭터 기본 설정
		animator = GetComponentInChildren<Animator>();
		animator.SetInteger("WeaponType_int", gunBlueprint.gunType);
		magazine = gunBlueprint.magazine;
		hp = playerBlueprint.hp;
	}

	void Update()
	{
		// 플레이어 입력
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		/*
		// 플레이어 이동 방향 설정 1-1 (상대좌표로 변경)
		Vector3 dir = new Vector3(h, 0, v);
		dir = dir.normalized;
		
		// 카메라를 기준으로 방향 설정 1-2
		dir = Camera.main.transform.TransformDirection(dir);
		*/

		// 점프가 끝났는지 확인
		if (isJumping && player.collisionFlags == CollisionFlags.Below)
		{
			// 점프 전 상태로 초기화한다.
			isJumping = false;
			// 캐릭터 수직 속도를 0으로 만든다.
			velocity.y = 0;
		}
		// 점프를 하고 있지 않다면 스페이스 바를 눌렀을 시 점프
		if (Input.GetButtonDown("Jump") && !isJumping)
		{
			// 캐릭터 수직 속도에 점프력을 적용하고 점프 상태로 변경한다.
			velocity.y = jumpHeight;
			isJumping = true;
		}

		
		// 캐릭터 수직 속도에 중력 값을 적용한다.
		//velocity.y += gravity * Time.deltaTime;
		//dir.y = velocity.y;
		

		// 플레이어 움직임 제어
		Vector3 move = transform.right * h + transform.forward * v;
		player.Move(move * speed * Time.deltaTime);

		// 캐릭터의 이동 애니메이션 제어
		velocity.y += gravity * Time.deltaTime;
		player.Move(velocity * Time.deltaTime);
		// 움직이고 있는지 확인
		if (h != 0 || v != 0)
		{
			isMoving = 1;
		}
		else
		{
			isMoving = 0;
		}
		speed = Mathf.Lerp(0.0f, playerBlueprint.speed, isMoving * 0.3f);
		animator.SetFloat("Speed_f", speed);

		// 캐릭터의 공격 애니메이션 설정
		animator.SetBool("Shoot_b", isClick);


		isClick = Input.GetButton("Fire1");
		// 캐릭터 공격
		if (isClick && !isFireRate && !isReload)
		{
			bullet = Instantiate(gunBlueprint.bullet, firePoint.transform.position, firePoint.transform.rotation);
			bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * gunBlueprint.bulletSpeed);
			Destroy(bullet, 2.0f);

			//CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 0f); 카메라 흔들림 제어

			magazine--;
			if (magazine == 0)
			{
				isReload = true;
				timer = gunBlueprint.reloadTime;
			}
			else
			{
				isFireRate = true;
				timer = gunBlueprint.fireRate;

			}
		}

		if (timer >= 0)
		{
			timer -= Time.deltaTime;
		}
		else if(isFireRate)
		{
			isFireRate = false;
		}
		else if(isReload)
		{
			isReload = false;
			magazine = gunBlueprint.magazine;
		}
	}

	void isAttacked(int damage){
		hp -= damage;
		if(hp <= 0){
			//플레이어 사망
		}
	}
}
