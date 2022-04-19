using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Defalt_Enemy_Controller : MonoBehaviour
{
    Animator Enemyanimator; // 애니메이터 
 
    PlayerHpManager playerHpManager;

    public GameObject[] players;

    public Enemy_Status1 enemy_Status;

    NavMeshAgent nav;
    Rigidbody rigid;

    public Transform target; // 플레이어 추적
    public Transform point; // 포인트 추적 
    public GameObject healingobj;
    public GameObject bloodobj;
    
    bool Move;

    bool isdelay;
    float health;

    Vector3 reactVec;
    //int atkStep;  // 공격 모션 단계  


    void Awake()
    {
        Enemyanimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Start()
    {
        //e_status = FindObjectOfType<Enemy_Status>();
        //p_status = FindObjectOfType<Player_Status>();
        health = enemy_Status.defalt_Health;
        Move = true;
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
        //if (target)
        //{
        //    Enemyanimator.SetBool("Forward", true);
        //    nav.SetDestination(target.position);
        //    if ((target.position - transform.position).magnitude < 3)
        //    {
        //        Enemyanimator.SetBool("Forward", false);
        //    }
        //}
        if ((target.position - transform.position).magnitude < (point.position - transform.position).magnitude)
        {
            if ((target.position - transform.position).magnitude >= 3)
            {
                Enemyanimator.SetBool("Forward", true);
                nav.SetDestination(target.position);
                //transform.Translate(Vector3.forward * e_status.defalt_Speed * Time.deltaTime, Space.Self);
            }
            if ((target.position - transform.position).magnitude < 3)
            {
                Enemyanimator.SetBool("Forward", false);
            }
        }

        if ((target.position - transform.position).magnitude >= (point.position - transform.position).magnitude)
        {
            if ((point.position - transform.position).magnitude >= 3)
            {
                Enemyanimator.SetBool("Forward", true);
                nav.SetDestination(point.position);
                //transform.Translate(Vector3.forward * e_status.defalt_Speed * Time.deltaTime, Space.Self);
            }

            if ((point.position - transform.position).magnitude < 3)
            {
                Enemyanimator.SetBool("Forward", false);
            }
        }
    }

    void Target()
    {
        Transform near_p  = null;

        //target = players[Random.Range(0, players.Length)].transform;
        foreach (GameObject p in players)
        {
            if (Vector3.Distance(transform.position, p.transform.position) <= 30f)
            {
                if (!near_p || Vector3.Distance(p.transform.position, transform.position)< Vector3.Distance(near_p.position, transform.position))
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

        //Debug.Log("[DEC]Target / target 추적" + target.name);
        //Debug.Log("[DEC]Target / Distance : " + Vector3.Distance(transform.position, near_p.position));
        //target = GameObject.FindWithTag("Player").transform; // 1인모드 시 필요
    }
    // Update is called once per frame
    void Update()
    {
        Target();
        if (Move)
        {
            Debug.Log("[DEC]Target / target 추적" + target.name);
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

            Debug.Log("[TEC]Enemy_Attack / Attack");
            Enemyanimator.Play("Bite Attack");
        }
        if ((point.position - transform.position).magnitude <= 3)
        {

            Debug.Log("[TEC]Enemy_Attack / Attack");
            Enemyanimator.Play("Bite Attack");
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
        int h;
        h = (int)Random.Range(0, 9);
        if (h == 1)
        {
            Instantiate(healingobj, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
        }
        
        GameManager.instance.enemy_Death++;
        GameManager.instance.score += 50;
        Debug.Log("[DEC]Death / Death : " + GameManager.instance.enemy_Death);
        nav.speed = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("[DEC]OnTriggerEnter / test");
        if (other.tag == "Bullet")
        {
            Instantiate(bloodobj, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(other.gameObject.transform.rotation.x, other.gameObject.transform.rotation.y, other.gameObject.transform.rotation.z));
            Destroy(other.gameObject);
            health -= 35;
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
