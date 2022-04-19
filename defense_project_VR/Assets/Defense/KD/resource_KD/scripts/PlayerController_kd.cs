using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_kd : MonoBehaviour
{

    public Enemy_Status1 e_status;
    public string playerType;

    public Player_Status p_status;
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
    private GameObject bulletEffect;

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
            if (Input.GetButtonDown("Jump") ||  OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.Touch) > 0 && !isJumping)
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
            isClick = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch) > 0;
            //isClick = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger,OVRInput.Controller.Touch);
            // 캐릭터 공격
            //if (isClick && !isFireRate && !isReload || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            if (isClick && !isFireRate && !isReload)
            {
                bullet = Instantiate(gunBlueprint.bullet, firePoint.transform.position, firePoint.transform.rotation);
                bulletEffect = Instantiate(gunBlueprint.bulletEffect, firePoint.transform.position, firePoint.transform.rotation);//bulletEffect
                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * gunBlueprint.bulletSpeed);
                Destroy(bullet, 2.0f);

                SoundManger.instance.ShootUp();

                //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 0f); 카메라 흔들림 제어

                magazine--;
                if ((magazine == 0 || (Input.GetKeyDown(KeyCode.R) || OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger,
                    OVRInput.Controller.RTouch) && magazine != gunBlueprint.magazine)) && !isReload)  // 자동 재장전 or 수동 재장전
                {
                    SoundManger.instance.GunReloding();
                    isReload = true;
                    animator.SetBool("Reload_b", isReload);
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
            GameManager.instance.gameover = true;
            //animator.SetInteger("DeathType_int", 2);
        }
        //animator.SetBool("Death", isdeath);
    }



    void Setup()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetInteger("WeaponType_int", gunBlueprint.gunType);
        magazine = gunBlueprint.magazine;
        //p_status = FindObjectOfType<Player_Status>();
        //e_status = FindObjectOfType<Enemy_Status>();
        Debug.Log("[PlayreController]Setup/ playerType" + playerType);

        cur_hp = 100;
        max_hp = 100;   
        //if (playerType == "Player1")
        //{
        //    cur_hp = PlayerHpManager.instance.player1_cur_Hp;
        //    max_hp = PlayerHpManager.instance.player1_max_Hp;
        //}
        //if (playerType == "Player2")
        //{
        //    cur_hp = PlayerHpManager.instance.player2_cur_Hp;
        //    max_hp = PlayerHpManager.instance.player2_max_Hp;
        //}
        //if (playerType == "Player3")
        //{
        //    cur_hp = PlayerHpManager.instance.player3_cur_Hp;
        //    max_hp = PlayerHpManager.instance.player3_max_Hp;
        //}
        //if (playerType == "Player4")
        //{
        //    cur_hp = PlayerHpManager.instance.player4_cur_Hp;
        //    max_hp = PlayerHpManager.instance.player4_max_Hp;
        //}


        //cur_hp = playerHpManager.player1_;
        Debug.Log("[PlayreController]OntriggerEnter/cur_hp : " + cur_hp);
        //max_hp = p_status.defalt_Health;
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



        if (other.tag == "Map2")
        {
            Debug.Log("[PlayreController]" + playerType + "_OntriggerEnter/nextMap : " + GameManager.instance.nextMap);
            GameManager.instance.nextMap = true;
            GameManager.instance.flare.SetActive(false);
        }
        if (cur_hp < 0)
        {
            if (other.tag == "Player")
            {
                Debug.Log("[PlayreController]" + playerType + "_OntriggerEnter/Resurrection : ");
                Resurrection();
            }
        }
        if (other.tag == "Heal" && isheal == true)
        {
            Debug.Log("[PlayreController]" + playerType + "_OntriggerEnter/Healobj : ");
            isheal = false;
            StartCoroutine(Heal());

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy_atk")// 기본형
        {

            Debug.Log("[PlayreController]" + playerType + "_OntriggerEnter_e.status." + e_status.enemyType1 + "_Damage : " + e_status.defalt_Damage);
            cur_hp -= e_status.defalt_Damage;

            Debug.Log(playerType + "_cur_hp : " + cur_hp);
        }

        if (other.tag == "Aerial_atk")// 공중형
        {

            Debug.Log("[PlayreController]" + playerType + "_OntriggerEnter_e.status." + e_status.enemyType1 + "_Damage : " + e_status.aerial_Damage);
            cur_hp -= e_status.aerial_Damage;

            Debug.Log(playerType + "_cur_hp : " + cur_hp);
        }
        if (other.tag == "Speed_atk")// 속도형
        {
            Debug.Log("[PlayreController]" + playerType + "_OntriggerEnter/e.status.speed_Damage : " + e_status.speed_Damage);
            cur_hp -= e_status.speed_Damage;

            Debug.Log("Speed_atk : " + cur_hp);
        }
        if (other.tag == "Physical_atk") // 체력형
        {
            Debug.Log("[PlayreController]" + playerType + "_OntriggerEnter/e.status.physical_Damage : " + e_status.physical_Damage);
            cur_hp -= e_status.physical_Damage;

            Debug.Log("physical_Damage : " + cur_hp);
        }
        if (other.tag == "Reinforced_atk") // 강화형
        {
            Debug.Log("[PlayreController]" + playerType + "_OntriggerEnter/e.status.reinforced_Damage : " + e_status.reinforced_Damage);
            cur_hp -= e_status.reinforced_Damage;

            Debug.Log("Reinforced_atk : " + cur_hp);
        }
        if (other.tag == "Middle_atk") // 중간보스
        {
            Debug.Log("[PlayreController]" + playerType + "_OntriggerEnter/e.status.middle_Damage : " + e_status.middle_Damage);
            cur_hp -= e_status.middle_Damage;

            Debug.Log("Middle_atk : " + cur_hp);
        }

        if (other.tag == "Final_atk")
        {
            Debug.Log("[PlayreController]" + playerType + "_OntriggerEnter/e.status.final_Damage : " + e_status.final_Damage);
            cur_hp -= e_status.final_Damage;

            Debug.Log("Final_atk : " + cur_hp);
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
        Debug.Log("[PlayreController]" + playerType + "Resurrection : " + cur_hp);
        isdeath = true;

        //animator.SetInteger("DeathType_int", 0);
        animator.SetBool("Death_b", false);
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        GameObject healobj = Instantiate(effectObj, transform.position, effectObj.transform.rotation);
        Destroy(healobj, 1f);

    }

    IEnumerator Heal()
    {
        yield return new WaitForSeconds(1f);
        if (max_hp < cur_hp)
        {
            cur_hp = max_hp;
            Debug.Log("[PlayreController]" + playerType + "_OntriggerEnter/Heal : " + cur_hp);
        }
        else
        {
            cur_hp += 30;
            Debug.Log("[PlayreController]" + playerType + "_OntriggerEnter/Heal : " + cur_hp);
        }
        isheal = true;
        GameObject healobj = Instantiate(effectObj, transform.position, effectObj.transform.rotation);
        Destroy(healobj, 1f);
    }


}
