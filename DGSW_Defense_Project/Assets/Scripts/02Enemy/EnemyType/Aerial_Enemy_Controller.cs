using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aerial_Enemy_Controller : MonoBehaviour
{
    Animator Enemyanimator; // 애니메이터 
    Enemy_Status e_status; // Enenmy 상태
    public Transform target; // 추적 대상
    public Transform point; // 포인트 추적
    private float speed; // 이동속도
    bool Move;
    int atkStep;  // 공격 모션 단계  


    void Awake()
    {
        Enemyanimator = GetComponent<Animator>();
    }


    void Start()
    {
        e_status = FindObjectOfType<Enemy_Status>();
        Move = true;
        target = GameObject.FindWithTag("Player").transform; // 추적 대상 위치
        point = GameObject.FindWithTag("Defanse_Point").transform; // 추적 대상 위치
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
                Enemyanimator.SetBool("Forward", true);
                transform.Translate(Vector3.forward * e_status.defalt_Speed * Time.deltaTime, Space.Self);
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
                transform.Translate(Vector3.forward * e_status.defalt_Speed * Time.deltaTime, Space.Self);
            }

            if ((point.position - transform.position).magnitude < 3)
            {
                Enemyanimator.SetBool("Forward", false);
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

            Debug.Log("[PEC]Enemy_Attack / Attack");
            switch (atkStep)
            {
                case 0:
                    atkStep += 1;
                    Enemyanimator.Play("Bite Attack");
                    break;
                case 1:
                    atkStep = 0; ;
                    Enemyanimator.Play("Tail Attack");
                    break;

            }
        }
    }

    void freezeenemy()
    {
        Debug.Log("[AEC]freezeenemy / Freeze");
        Move = false;
    }
    void unfreezeenemy()
    {
        Debug.Log("[AEC]unfreezeenemy / UnFreeze");
        Invoke("Move_Ture", 3f);
    }

    void Move_Ture()
    {
        Move = true;
    }
}
