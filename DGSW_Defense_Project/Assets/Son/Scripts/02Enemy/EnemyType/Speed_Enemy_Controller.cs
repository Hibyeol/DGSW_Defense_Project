using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Speed_Enemy_Controller: MonoBehaviour
{
    Animator Enemyanimator; // 애니메이터 
    Enemy_Status1 e_status; // Enenmy 상태
    Rigidbody rigid;
    NavMeshAgent nav;

    public Transform target; // 추적 대상
    public Transform point; // 포인트 추적
    
    private float speed; // 이동속도

    bool Move;
    bool isdelay;
    float health;
    int atkStep;  // 공격 모션 단계  


    void Awake()
    {
        Enemyanimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Start()
    {
        Move = true;
        health = e_status.speed_Health;
        target = GameObject.FindWithTag("Player").transform; // 추적 대상 위치
        point = GameObject.FindWithTag("Defanse_Point").transform; // 추적 대상 위치
        isdelay = true;
    }
    void RotateEnemy()
    {
        if ((target.position - transform.position).magnitude >= (point.position - transform.position).magnitude)
        {
            Vector3 dir = point.position - transform.position;
            transform.localRotation =
                Quaternion.Slerp(transform.localRotation,
                    Quaternion.LookRotation(dir), 5 * Time.deltaTime);
        }
        else if ((target.position - transform.position).magnitude < (point.position - transform.position).magnitude)
        {
            Vector3 dir = target.position - transform.position;
            transform.localRotation =
                Quaternion.Slerp(transform.localRotation,
                    Quaternion.LookRotation(dir), 5 * Time.deltaTime);
        }
    }


    void EnemyMove()
    {
        if ((target.position - transform.position).magnitude < (point.position - transform.position).magnitude)
        {
            if ((target.position - transform.position).magnitude >= 3)
            {
                Enemyanimator.SetBool("Move Forward Slow", true);
                nav.SetDestination(target.position);
                //transform.Translate(Vector3.forward * e_status.speed_Speed * Time.deltaTime, Space.Self);
            }

            if ((target.position - transform.position).magnitude < 3)
            {
                Enemyanimator.SetBool("Move Forward Slow", false);
            }
        }

        if ((target.position - transform.position).magnitude >= (point.position - transform.position).magnitude)
        {
            if ((point.position - transform.position).magnitude >= 3)
            {
                Enemyanimator.SetBool("Move Forward Slow", true);
                nav.SetDestination(point.position);
                //transform.Translate(Vector3.forward * e_status.speed_Speed * Time.deltaTime, Space.Self);
            }

            if ((point.position - transform.position).magnitude < 3)
            {
                Enemyanimator.SetBool("Move Forward Slow", false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Move)
        {
            //RotateEnemy();
            EnemyMove();
        }
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }
    void EnemyAttack()
    {
        if ((target.position - transform.position).magnitude <= 3)
        {

            Debug.Log("[SEC]Enemy_Attack / Attack");
            switch (atkStep)
            {
                case 0:
                    atkStep += 1;
                    Enemyanimator.Play("Projectile Attack 01");
                    break;
                case 1:
                    atkStep = 0; ;
                    Enemyanimator.Play("Projectile Attack 02");
                    break;

            }
        }
        if ((point.position - transform.position).magnitude <= 3)
        {

            Debug.Log("[SEC]Enemy_Attack / Attack");
            switch (atkStep)
            {
                case 0:
                    atkStep += 1;
                    Enemyanimator.Play("Projectile Attack 01");
                    break;
                case 1:
                    atkStep = 0; ;
                    Enemyanimator.Play("Projectile Attack 02");
                    break;

            }
        }
    }

    void freezeenemy()
    {
        Debug.Log("[SEC]freezeenemy / Freeze");
        Move = false;
    }
    void unfreezeenemy()
    {
        Debug.Log("[SEC]unfreezeenemy / UnFreeze");
        Invoke("Move_Ture", 3f);
    }

    void Move_Ture()
    {
        Move = true;
    }

    void Death()
    {
        if (health < 0)
        {
            Enemyanimator.Play("Die");

            Destroy(gameObject, 3f);
            Destroy(gameObject, 3f);
            GameManager.instance.enemy_Death++;
            GameManager.instance.score += 120;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[SEC]OnTriggerEnter / test");
        if (other.tag == "Bullet")
        {

            health -= 35;
            //health -= p_status.defalt_Damage;
            Debug.Log("[SEC]OnTriggerEnter / health : " + health);
            if (health <= 0 && isdelay == true)
            {
                Death();
                isdelay = false;

            }
        }
    }
    IEnumerator CountDeathDelay()
    {
        yield return new WaitForSeconds(3f);
        isdelay = false;
    }
}