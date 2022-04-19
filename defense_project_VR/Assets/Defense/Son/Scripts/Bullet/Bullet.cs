    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float speed = 8f;
    private Rigidbody bulletRigidbody;

    Defalt_Enemy_Controller dec;

    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();// 게임 오브잭트에서 컴포넌트를 찾아 할당
        bulletRigidbody.velocity = transform.forward * speed;

        Destroy(gameObject, 10f); // 3초뒤 파괴
    }

    // Update is called once per frame
    void Update()
    {   

    }

    private void OnTriggerEnter(Collider other) // 트리거 충돌 시 자동 실행
    {
        if (other.tag == "Enemy_HitBox")
        {
            Destroy(gameObject); 
        }
    }
}
