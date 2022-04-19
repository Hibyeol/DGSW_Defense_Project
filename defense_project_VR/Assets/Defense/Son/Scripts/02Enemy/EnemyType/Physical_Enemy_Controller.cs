using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Physical_Enemy_Controller : MonoBehaviour
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
    int atkStep;  // 공격 모션 단계  
    bool isdelay;
    float health;

    void Awake()
    {
        Enemyanimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Start()
    {
        health = e_status.physical_Health;
        Move = true;
        //target = GameObject.FindWithTag("Player").transform;
        players = GameObject.FindGameObjectsWithTag("Player");
        point = GameObject.FindWithTag("Defanse_Point").transform;
        isdelay = true;
    }
    void RotateEnemy()
    {
        Vector3 dir = target.position - transform.position;
        transform.localRotation =
            Quaternion.Slerp(transform.localRotation,
                Quaternion.LookRotation(dir), 5 * Time.deltaTime);
    }


    void EnemyMove()
    {
        if ((target.position - transform.position).magnitude < (point.position - transform.position).magnitude)
        {
            if ((target.position - transform.position).magnitude >= 3)
            {
                Enemyanimator.SetBool("Slither Forward", true);
                nav.SetDestination(target.position);
                //transform.Translate(Vector3.forward * e_status.defalt_Speed * Time.deltaTime, Space.Self);
            }

            if ((target.position - transform.position).magnitude < 3)
            {
                Enemyanimator.SetBool("Slither Forward", false);
            }
        }

        if ((target.position - transform.position).magnitude >= (point.position - transform.position).magnitude)
        {
            if ((point.position - transform.position).magnitude >= 3)
            {
                Enemyanimator.SetBool("Slither Forward", true);
                nav.SetDestination(point.position);
                //transform.Translate(Vector3.forward * e_status.defalt_Speed * Time.deltaTime, Space.Self);
            }

            if ((point.position - transform.position).magnitude < 3)
            {
                Enemyanimator.SetBool("Slither Forward", false);
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
        if (Move)
        {
            Target();
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

            Debug.Log("[PEC]Enemy_Attack / Attack");
            switch (atkStep)
            {
                case 0:
                    atkStep += 1;
                    Enemyanimator.Play("Claw Attack Left");
                    break;
                case 1:
                    atkStep = 0; ;
                    Enemyanimator.Play("Claw Attack Right");
                    break;

            }
        }
        if ((point.position - transform.position).magnitude <= 3)
        {

            Debug.Log("[PEC]Enemy_Attack / Attack");
            switch (atkStep)
            {
                case 0:
                    atkStep += 1;
                    Enemyanimator.Play("Claw Attack Left");
                    break;
                case 1:
                    atkStep = 0; ;
                    Enemyanimator.Play("Claw Attack Right");
                    break;

            }
        }
    }

    void freezeenemy()
    {
        Debug.Log("[DEC]freezeenemy / Freeze");
        Move = false;
    }
    void unfreezeenemy()
    {
        Debug.Log("[DEC]unfreezeenemy / UnFreeze");
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
        GameManager.instance.score += 100;
        int h;
        h = (int)Random.Range(0, 9);
        if (h == 1)
        {
            Instantiate(healingobj, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
        }
        Debug.Log("[DEC]Death / Death : " + GameManager.instance.enemy_Death);
        nav.speed = 0; //죽으면 움직이는 속도 0
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("[DEC]OnTriggerEnter / test");
        if (other.tag == "Bullet")
        {

            Destroy(other.gameObject);

            //reactVec = transform.position - other.transform.position;
            health -= 35;
            //reactVec = reactVec.normalized;
            //reactVec.y = 0;
            //rigid.AddForce(reactVec * 1f, ForceMode.Impulse);

            //health -= p_status.defalt_Damage;

            if (health <= 0 && isdelay == true)
            {
                Death();
                isdelay = false;

            }
        }
    }
    IEnumerator CountDeathDelay()
    {
        yield return new WaitForSeconds(5f);
        isdelay = false;
    }
}
