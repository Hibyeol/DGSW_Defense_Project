using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager_kd : MonoBehaviour
{

    public GameObject bullet;
    public GameObject bulletEffect;
    GameObject bE;
    float bulletSpeed = 10.0f;

    public GunBlueprint_kd gunBlueprint_kd;
    public GameObject firePoint;
    public Transform aim;

    private Animator animator;

    bool ifClick = false;
    bool ifFireRate = false;
    bool reload = false;

    float timer = 0.0f;

    int magazine;

    void Start()
    {
        Setup();
    }

    void FixedUpdate()
    {
        ifClick = Input.GetButton("Fire1");
    }
    // Update is called once per frame
    void Update()
    {
        Fire();
        FireRateCountdown();
    }

    void Fire()
    {
        animator.SetBool("Shoot_b", ifClick);
        Debug.Log(firePoint.transform.position);
        if(ifClick && !ifFireRate){
            GameObject b = Instantiate(bullet, firePoint.transform.position,Quaternion.identity); // bullet
            bE = Instantiate(bulletEffect, firePoint.transform.position, Quaternion.identity);//bulletEffect
            Destroy(bE, 0.3f);
            b.GetComponent<Rigidbody>().AddForce(b.transform.forward * bulletSpeed);

            magazine--;
            if (magazine == 0)
            {
                reload = true;
            }
            else {
                timer = gunBlueprint_kd.fireRate;
                ifFireRate = true;
			}
        }
    }

    void FireRateCountdown()
    {
        if (timer >= 0){
            timer -= Time.deltaTime;
		}
        else {
            ifFireRate = false;
		}
	}

    void Setup()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetInteger("WeaponType_int", gunBlueprint_kd.gunType);
        magazine = gunBlueprint_kd.magazine;
        aim.localPosition = new Vector3(0.0f, 0.0f, gunBlueprint_kd.fireRange);
    }


}
