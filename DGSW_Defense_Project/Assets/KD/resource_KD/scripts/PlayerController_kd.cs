using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_kd : MonoBehaviour
{
	Player_Status p_status;
	Enemy_Status e_status;

	public PlayerBlueprint_kd playerBlueprint;

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

	private float cur_hp;// 현재 자신의 체력
	private float max_hp;// 현재 자신의 체력

	bool isGrounded;
	bool isJumping;

	int isMoving;

	public Animator animator;

	private GameObject bullet;

	public GunBlueprint_kd gunBlueprint;
	public GameObject firePoint;
	public Transform aim;

	bool isClick = false;
	bool isFireRate = false;
	bool isReload = false;
	bool isdeath = true;
	bool isheal = true;

	float timer = 0.0f;

	int magazine;

	public GameObject effectObj;

	void Awake()
	{
		//aim.localPosition = new Vector3(0.0f, 0.0f, gunBlueprint.fireRange); // 에임 위치 설정
	}

	void Start()
	{
		Setup();

	}

	//void FixedUpdate()
	//{
	//	x = Input.GetAxis("Horizontal");
	//	z = Input.GetAxis("Vertical");
	//	jump = Input.GetButtonDown("Jump");
	//	ifClick = Input.GetButton("Fire1");
	//}

	void Update()
	{
		if (isdeath && GameManager.instance.isPmove)
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
			else if (isFireRate)
			{
				isFireRate = false;
			}
			else if (isReload)
			{
				isReload = false;
				magazine = gunBlueprint.magazine;
			}
		}
		if (cur_hp <= 0)
		{
			isdeath = false;
			animator.SetBool("Death_b", true);
		}
	}

	

	void Death()
	{
		if (cur_hp <= 0)
		{
			isdeath = false;
			animator.SetBool("Death_b", true);
		}
	}

	//void Fire()
	//{
	//	animator.SetBool("Shoot_b", isClick);
	//	//Debug.Log(firePoint.transform.position);
	//	if (isClick && !isFireRate)
	//	{
	//		bullet = Instantiate(gunBlueprint.bullet, firePoint.transform.position, firePoint.transform.rotation);
	//		bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * gunBlueprint.bulletSpeed);
	//		Destroy(bullet, 2.0f);

	//		magazine--;
	//		if (magazine == 0)
	//		{
	//			reload = true;
	//		}
	//		else
	//		{
	//			timer = gunBlueprint.fireRate;
	//			isFireRate = true;
	//		}
	//	}
	//}

	//void FireRateCountdown()
	//{
	//	if (timer >= 0)
	//	{
	//		timer -= Time.deltaTime;
	//	}
	//	else
	//	{
	//		isFireRate = false;
	//	}
	//}

	void Setup()
	{
		animator = GetComponentInChildren<Animator>();
		animator.SetInteger("WeaponType_int", gunBlueprint.gunType);
        magazine = gunBlueprint.magazine;
		p_status = FindObjectOfType<Player_Status>();
		e_status = FindObjectOfType<Enemy_Status>();
		cur_hp = p_status.defalt_Health;
		Debug.Log("[PlayreController]OntriggerEnter/cur_hp : " + cur_hp);
		max_hp = p_status.defalt_Health;
		Debug.Log("[PlayreController]OntriggerEnter/max_hp : " + max_hp);

	}

	public void HitByExplosion(Vector3 explosionPos) // 폭발형
	{
		cur_hp -= e_status.explosion_Damage;
		Debug.Log("Explosion_Enemy_atk : " + cur_hp);
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("[PlayreController]OntriggerEnter");
		//Debug.Log("[PlayreController]OnTriggerEnter/other : " + other);

		if (other.tag == "Enemy_atk")// 기본형
		{

			Debug.Log("[PlayreController]OntriggerEnter/e.status.defalt_Damage : " + e_status.defalt_Damage);
			cur_hp -= e_status.defalt_Damage;

			Debug.Log("Enemy_atk : " + cur_hp);
		}

		if (other.tag == "Aerial_atk")// 공중형
		{
			Debug.Log("[PlayreController]OntriggerEnter/e.status.aerial_Damage : " + e_status.aerial_Damage);
			cur_hp -= e_status.aerial_Damage;

			Debug.Log("Aerial_atk : " + cur_hp);
		}
		if (other.tag == "Speed_atk")// 속도형
		{
			Debug.Log("[PlayreController]OntriggerEnter/e.status.speed_Damage : " + e_status.speed_Damage);
			cur_hp -= e_status.speed_Damage;

			Debug.Log("Speed_atk : " + cur_hp);
		}
		if (other.tag == "Physical_atk") // 체력형
		{
			Debug.Log("[PlayreController]OntriggerEnter/e.status.physical_Damage : " + e_status.physical_Damage);
			cur_hp -= e_status.physical_Damage;

			Debug.Log("physical_Damage : " + cur_hp);
		}
		if (other.tag == "Reinforced_atk") // 강화형
		{
			Debug.Log("[PlayreController]OntriggerEnter/e.status.reinforced_Damage : " + e_status.reinforced_Damage);
			cur_hp -= e_status.reinforced_Damage;

			Debug.Log("Reinforced_atk : " + cur_hp);
		}
		if (other.tag == "Middle_atk") // 중간보스
		{
			Debug.Log("[PlayreController]OntriggerEnter/e.status.middle_Damage : " + e_status.middle_Damage);
			cur_hp -= e_status.middle_Damage;

			Debug.Log("Middle_atk : " + cur_hp);
		}
		if (other.tag== "Map2")
        {
			Debug.Log("[PlayreController]OntriggerEnter/nextMap : " + GameManager.instance.nextMap);
			GameManager.instance.nextMap = true;
        }
        if (cur_hp < 0)
        {
			if (other.tag == "Player")
			{
				Debug.Log("[PlayreController]OntriggerEnter/Resurrection ");
				Resurrection();
			}
		}
		if (other.tag == "Heal"&&isheal == true)
		{
			Debug.Log("[PlayreController]OntriggerEnter/Healobj : ");
			isheal = false;
			StartCoroutine(Heal());

		}
	}

 //   void Heal()
 //   {
	//	if (max_hp < cur_hp)
	//	{
	//		cur_hp = max_hp;
	//		Debug.Log("[PlayreController]OntriggerEnter/Heal : " + cur_hp);
	//	}
	//	else
	//	{
	//		cur_hp += 30;
	//		Debug.Log("[PlayreController]OntriggerEnter/Heal : " + cur_hp);
	//	}
	//	Instantiate(effectObj, transform.position, Quaternion.identity);
	//}

	void Resurrection()
    {
		cur_hp = 30;
		isdeath = false;
		Instantiate(effectObj, transform.position, Quaternion.identity);
	}

	IEnumerator Heal()
    {
		yield return new WaitForSeconds(1f);
		if (max_hp < cur_hp)
		{
			cur_hp = max_hp;
			Debug.Log("[PlayreController]OntriggerEnter/Heal : " + cur_hp);
		}
		else
		{
			cur_hp += 30;
			Debug.Log("[PlayreController]OntriggerEnter/Heal : " + cur_hp);
		}
		isheal = true;
		Instantiate(effectObj, transform.position, Quaternion.identity);
	}


}
