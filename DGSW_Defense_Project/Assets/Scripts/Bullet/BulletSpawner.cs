using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; // 생성할 원본 

    void Start()
    {


       
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // 누적된 시간이 생성주기와 같거나 크다면 
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);//
        }   

    }
}
