using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Reinforced_Enemy_Controller : MonoBehaviour
{
    Animator Enemyanimator; // 애니메이터 
    public Enemy_Status1 e_status; // Enenmy 상태



    public GameObject[] players;

    Rigidbody rigid;
    NavMeshAgent nav;

    public Transform target; // 추적 대상
    public Transform point; // 포인트 추적 
    public GameObject healingobj;

    private float speed; // 이동속도
    bool Move;
    bool isdelay;
    float health;
    Vector3 reactVec;
    int atkStep;  // 공격 모션 단계  


    void Awake()
    {
        Enemyanimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Start()
    {
     
        health = e_status.defalt_Health;
        Move = true;
        //target = GameObject.FindWithTag("Player").transform;
        players = GameObject.FindGameObjectsWithTag("Player");
        point = GameObject.FindWithTag("Defanse_Point").transform;
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
                Enemyanimator.SetBool("Walk Forward Slow", true);
                nav.SetDestination(target.position);
                //transform.Translate(Vector3.forward * e_status.reinforced_Speed * Time.deltaTime, Space.Self);
            }

            if ((target.position - transform.position).magnitude < 3)
            {
                Enemyanimator.SetBool("Walk Forward Slow", false);
            }
        }

        if ((target.position - transform.position).magnitude >= (point.position - transform.position).magnitude)
        {
            if ((point.position - transform.position).magnitude >= 3)
            {
                Enemyanimator.SetBool("Walk Forward Slow", true);
                nav.SetDestination(point.position);
                //transform.Translate(Vector3.forward * e_status.reinforced_Speed * Time.deltaTime, Space.Self);
            }

            if ((point.position - transform.position).magnitude < 3)
            {
                Enemyanimator.SetBool("Walk Forward Slow", false);
            }
        }
    }

    void Target()
    {
        Transform near_p = null;

        //target = players[Random.Range(0, players.Length)].transform;
        foreach (GameObject p in players)
        {
            if (Vector3.Distance(transform.position, p.transform.position) <= 10f)
            {
                if (!near_p || Vector3.Distance(p.transform.position, transform.position) < Vector3.Distance(near_p.position, transform.position))
                {
                    near_p = p.transform;

                }

            }
            else
            {
                near_p = point.transform;
            }

        }
        target = near_p;
    }
    // Update is called once per frame
    void Update()
    {
        Target();
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

            Debug.Log("[REC]Enemy_Attack / Attack");
            switch (atkStep)
            {
                case 0:
                    atkStep += 1;
                    Enemyanimator.Play("Front Legs Attack");
                    break;
                case 1:
                    atkStep +=1; ;
                    Enemyanimator.Play("Tail Stab Attack");
                    break;
                case 2:
                    atkStep = 0; ;
                    Enemyanimator.Play("Tail Flick Attack");
                    break;

            }
        }
        if ((point.position - transform.position).magnitude <= 3)
        {

            switch (atkStep)
            {
                case 0:
                    atkStep += 1;
                    Enemyanimator.Play("Front Legs Attack");
                    break;
                case 1:
                    atkStep = +1; ;
                    Enemyanimator.Play("Tail Stab Attack");
                    break;
                case 2:
                    atkStep = 0; ;
                    Enemyanimator.Play("Tail Flick Attack");
                    break;

            }
        }
    }

    void freezeenemy()
    {
        Debug.Log("[REC]freezeenemy / Freeze");
        Move = false;
    }
    void unfreezeenemy()
    {
        Debug.Log("[REC]unfreezeenemy / UnFreeze");
        Invoke("Move_Ture", 3f);
    }

    void Move_Ture()
    {
        Move = true;
    }

    void Death()
    {

        Enemyanimator.Play("Die");


        Destroy(gameObject, 3f);

        GameManager.instance.enemy_Death++;
        GameManager.instance.score += 250;
        int h;
        h = (int)Random.Range(0, 9);
        if (h == 1)
        {
            Instantiate(healingobj, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
        }
        Debug.Log("[REC]Death / Death : " + GameManager.instance.enemy_Death);
        nav.speed = 0; //죽으면 움직이는 속도 0
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[REC]OnTriggerEnter / test");
        if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            health -= 35;
            Debug.Log("[REC]OnTriggerEnter / health : " + health);
            if (health <= 0 && isdelay == true)
            {
                Death();
                isdelay = false;

            }
        }
    }
}