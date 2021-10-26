using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePoint : MonoBehaviour
{
    public float max_health= 5000f;
    public float cur_health = 5000f;
    

    //Test_EnemyController controller;
    Rigidbody rigid;
    BoxCollider boxCollider;
    Enemy_Status e_status;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        
    }

    void Start()
    {
        e_status = GetComponent<Enemy_Status>();
        //damge = status.defalt_Damage;
    }

    public void HitByExplosion(Vector3 explosionPos)
    {
        cur_health -= e_status.explosion_Damage;
        Debug.Log("Point Enemy_atk : " + cur_health);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Ontrigger");
        //Debug.Log("[DP]OnTriggerEnter/other : "+ other);

        if (other.tag == "Enemy_atk")
        {
            
            Debug.Log("[DP]OnTriggerEnter/status.defalt_Damage : " + e_status.defalt_Damage);
            cur_health -= e_status.defalt_Damage;

            Debug.Log("Enemy_atk : " + cur_health);
        }
        if (other.tag == "Aerial_atk")
        {

            Debug.Log("[DP]OnTriggerEnter/status.aerial_Damage : " + e_status.aerial_Damage);
            cur_health -= e_status.aerial_Damage;

            Debug.Log("Enemy_atk : " + cur_health);
        }
        if (other.tag == "Physical_atk")
        {

            Debug.Log("[DP]OnTriggerEnter/status.physical_Damage : " + e_status.physical_Damage);
            cur_health -= e_status.physical_Damage;

            Debug.Log("Enemy_atk : " + cur_health);
        }
        if (other.tag == "Speed_atk")
        {

            Debug.Log("[DP]OnTriggerEnter/status.speed_Damage : " + e_status.speed_Damage);
            cur_health -= e_status.speed_Damage;

            Debug.Log("Enemy_atk : " + cur_health);
        }
        if (other.tag == "Reinforced_atk")
        {

            Debug.Log("[DP]OnTriggerEnter/status.reinforced_Damage : " + e_status.reinforced_Damage);
            cur_health -= e_status.reinforced_Damage;

            Debug.Log("Reinforced_atk : " + cur_health);
        }
        if (other.tag == "Middle_atk")
        {

            Debug.Log("[DP]OnTriggerEnter/status.middle_Damage : " + e_status.middle_Damage);
            cur_health -= e_status.middle_Damage;

            Debug.Log("middle_Damage : " + cur_health);
        }
    }




}
