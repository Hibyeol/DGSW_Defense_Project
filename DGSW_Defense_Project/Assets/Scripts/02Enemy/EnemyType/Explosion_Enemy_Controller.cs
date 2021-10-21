using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Enemy_Controller : MonoBehaviour
{
    Animator Enemyanimator; // 애니메이터 
    Enemy_Status e_status; // Enenmy 상태
    Player_Status p_status;
    //public GameObject particle;
    public Transform target; // 플레이어 추적
    public Transform point; // 포인트 추적 
    private float speed; // 이동속도
    bool Move;
    bool isdelay;
    float health;
    //int atkStep;  // 공격 모션 단계  


    public GameObject effectObj;

    void Awake()
    {
        Enemyanimator = GetComponent<Animator>();
    }


    void Start()
    {
        e_status = FindObjectOfType<Enemy_Status>();
        p_status = FindObjectOfType<Player_Status>();
        health = e_status.explosion_Health;
        Move = true;
        target = GameObject.FindWithTag("Player").transform;
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
                Enemyanimator.SetBool("Walk Forward Fast", true);
                transform.Translate(Vector3.forward * e_status.explosion_Speed * Time.deltaTime, Space.Self);
            }

            if ((target.position - transform.position).magnitude < 3)
            {
                Enemyanimator.SetBool("Walk Forward Fast", false);
            }
        }

        if ((target.position - transform.position).magnitude >= (point.position - transform.position).magnitude)
        {
            if ((point.position - transform.position).magnitude >= 3)
            {
                Enemyanimator.SetBool("Walk Forward Fast", true);
                transform.Translate(Vector3.forward * e_status.explosion_Speed * Time.deltaTime, Space.Self);
            }

            if ((point.position - transform.position).magnitude < 3)
            {
                Enemyanimator.SetBool("Walk Forward Fast", false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Move)
        {
            RotateEnemy();
            EnemyMove();
        }
    }

    void EnemyAttack()
    {
        if ((target.position - transform.position).magnitude <= 3)
        {

            Debug.Log("[EEC]Enemy_Attack / Attack");
            //Enemyanimator.Play("Bite Attack");
            StartCoroutine(Explosion());
            //GameObject bullet = Instantiate(particle, transform.position, transform.rotation);
        }
        if ((point.position - transform.position).magnitude <= 3)
        {

            Debug.Log("[EEC]Enemy_Attack / Attack");
            //Enemyanimator.Play("Bite Attack");
            StartCoroutine(Explosion());
        }
    }

    void freezeenemy()
    {
        Debug.Log("[EEC]freezeenemy / Freeze");
        Move = false;
    }
    void unfreezeenemy()
    {
        Debug.Log("[EEC]unfreezeenemy / UnFreeze");
        Invoke("Move_Ture", 3f);
    }

    void Move_Ture()
    {
        Move = true;
    }

    void Death()
    {

        Enemyanimator.Play("Die");
        StartCoroutine(Explosion());

        Debug.Log("[EEC]Death / Death : " + GameManager.instance.enemy_Death);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[EEC]OnTriggerEnter / test");
        if (other.tag == "Bullet")
        {

            health -= 35;
            //health -= p_status.defalt_Damage;
            Debug.Log("[EEC]OnTriggerEnter / health : " + health);
            if (health <= 0)
            {
                Death();
            }
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("[EEC]Explosion / Boom");
        Instantiate(effectObj, transform.position, Quaternion.identity);
        RaycastHit[] rayHitPoint = Physics.SphereCastAll(transform.position, 15, Vector3.up, 0f, LayerMask.GetMask("Point"));
        foreach(RaycastHit hitobj in rayHitPoint)
        {
            //hitobj.transform.GetComponent<PlayerController>().HitByExplosion(transform.position);
            hitobj.transform.GetComponent<DefensePoint>().HitByExplosion(transform.position);
        }

        RaycastHit[] rayHitPlayer = Physics.SphereCastAll(transform.position, 15, Vector3.up, 0f, LayerMask.GetMask("Player"));
        foreach (RaycastHit hitobj in rayHitPlayer)
        {
            hitobj.transform.GetComponent<PlayerController>().HitByExplosion(transform.position);
            //hitobj.transform.GetComponent<DefensePoint>().HitByExplosion(transform.position);
        }

        Destroy(gameObject);
        GameManager.instance.enemy_Death++;
    }

    IEnumerator CountDeathDelay()
    {
        yield return new WaitForSeconds(5f);
        isdelay = false;
    }
}
