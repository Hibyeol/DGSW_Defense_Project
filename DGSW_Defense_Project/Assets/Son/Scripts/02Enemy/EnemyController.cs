using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Animations;

public class EnemyController : MonoBehaviour
{
    public float health = 50f;
    public float speed = 3f;
    public float attack = 5f;
    public float speed_attack = 1f;
    private Rigidbody MonsterRigidbody;
    private bool Move;
    private Animator animator;


    //public Transform target;
    // public Vector3 direction;

    void Start()
    {
        MonsterRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //MonsterRigidbody.velocity = transform.forward * speed;
        Move = true;
        //Destroy(gameObject, 3f); // 3초뒤 파괴
    }

    void Update()
    {
        EnemyMove();

        //Debug.Log("test");

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Move = false;

            Attack();
            Debug.Log("test2");
        }
    }

    private void OnTriggerExit(Collider other)
    {

        Move = true;
    }

    public void Attack()
    {
        animator.SetBool("Smash Attack", true);
    }

    public void EnemyMove()
    {
        if (Move == true)
        {
            animator.SetBool("Walk Forward", true);
            MonsterRigidbody.velocity = transform.forward * speed;
        }
        else if (Move == false)
        {
            animator.SetBool("Walk Forward", false);
            MonsterRigidbody.velocity = transform.forward * speed * 0;
        }
    }


    public void EnemyReMove()
    {
        Invoke("EnemyMove", 2f);
    }
}