using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_EnemyController : MonoBehaviour
{
    Animator Enemyanimator; // 애니메이터 
    //private GameObject Enemy;
    //private GameObject Enemy2;
    Enemy_Status e_status; // Enenmy 상태
    public Transform target; // 추적 대상
    private float speed; // 이동속도
    //public float damge; // 공격력
    bool Move;
    int atkStep;  // 공격 모션 단계  

    // Start is called before the first frame update
    void Awake()
    {
        Enemyanimator = GetComponent<Animator>();
        //damge = status.defalt_Damage;
    }


    void Start()
    {
        //Enemy = GameObject.FindGameObjectWithTag("Defalt_Enemy");
        //Enemy2 = GameObject.FindGameObjectWithTag("Physical_Enemy");
        e_status = FindObjectOfType<Enemy_Status>();
        Move = true;
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
        if((target.position - transform.position).magnitude >= 3)
        {
            Enemyanimator.SetBool("Forward", true);
            transform.Translate(Vector3.forward * e_status.defalt_Speed * Time.deltaTime, Space.Self);
        }

        if((target.position - transform.position).magnitude < 3)
        {
            Enemyanimator.SetBool("Run Forward", false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Move)
        {
            RotateEnemy();
            //EnemyMove();
            Defalt_EnemyMove();
        }
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"), true); //레이어 겹치지 않기 위한 코드
       // Debug.Log("Layer");    
    }

    void Defalt_EnemyMove()
    {
        //if (Enemy == true)
        //{
            if ((target.position - transform.position).magnitude >= 3)
            {
                Enemyanimator.SetBool("Forward", true);
                transform.Translate(Vector3.forward * e_status.defalt_Speed * Time.deltaTime, Space.Self);
            }

            if ((target.position - transform.position).magnitude < 3)
            {
                Enemyanimator.SetBool("Forward", false);
            }
        //}
    }



    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.transform.tag == "Player")
    //    {
    //        Move = false;

    //        EnemyAttack();
    //        Debug.Log("test2");
    //    }
    //}
    void EnemyAttack()
    {
        if ((target.position - transform.position).magnitude <= 3)
        {
           
                Debug.Log("[TEC]Enemy_Attack / Attack");
                switch (atkStep)
                {
                    case 0:
                        atkStep += 1;
                        Enemyanimator.Play("Stab Attack");
                        break;
                    case 1:
                        atkStep = 0; ;
                        Enemyanimator.Play("Smash Attack");
                        break;

                }
                //Enemyanimator.Play("Stab Attack");
                //Enemyanimator.Play("Bite Attack");
            
            
                //Enemyanimator.Play("Smash Attack");

        }
    }

    void freezeenemy()
    {
        Debug.Log("[TEC]freezeenemy / Freeze");
        Move = false;
    }
    void unfreezeenemy()
    {
        Debug.Log("[TEC]unfreezeenemy / UnFreeze");
        Invoke("Move_Ture",3f);
    }

    void Move_Ture()
    {
        Move = true;
    }

}
