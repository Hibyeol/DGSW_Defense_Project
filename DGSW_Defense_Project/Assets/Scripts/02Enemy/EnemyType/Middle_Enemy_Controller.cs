﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Middle_Enemy_Controller : MonoBehaviour
{
    Animator Enemyanimator; // 애니메이터 
    Enemy_Status e_status; // Enenmy 상태
    Player_Status p_status;
    NavMeshAgent nav;
    Rigidbody rigid;

    public Transform target; // 플레이어 추적
    public Transform point; // 포인트 추적 

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
        e_status = FindObjectOfType<Enemy_Status>();
        p_status = FindObjectOfType<Player_Status>();
        health = e_status.defalt_Health;
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
                Enemyanimator.SetBool("Walk Forward Slow", true);
                nav.SetDestination(target.position);
                //transform.Translate(Vector3.forward * e_status.defalt_Speed * Time.deltaTime, Space.Self);
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
                //transform.Translate(Vector3.forward * e_status.defalt_Speed * Time.deltaTime, Space.Self);
            }

            if ((point.position - transform.position).magnitude < 3)
            {
                Enemyanimator.SetBool("Walk Forward Slow", false);
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

            Debug.Log("[MEC]Enemy_Attack / Attack");
            Enemyanimator.Play("Bite Attack");
        }
        if ((point.position - transform.position).magnitude <= 3)
        {

            Debug.Log("[MEC]Enemy_Attack / Attack");
            Enemyanimator.Play("Bite Attack");
        }
    }

    void freezeenemy()
    {
        Debug.Log("[MEC]freezeenemy / Freeze");
        Move = false;
    }
    void unfreezeenemy()
    {
        Debug.Log("[MEC]unfreezeenemy / UnFreeze");
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
        GameManager.instance.score += 50;
        Debug.Log("[MEC]Death / Death : " + GameManager.instance.enemy_Death);

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