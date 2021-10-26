using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GameObject bullet;

    public GunBlueprint gunBlueprint;
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
        if (ifClick && !ifFireRate)
        {
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            magazine--;
            if (magazine == 0)
            {
                reload = true;
            }
            else
            {
                timer = gunBlueprint.fireRate;
                ifFireRate = true;
            }
        }
    }

    void FireRateCountdown()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            ifFireRate = false;
        }
    }

    void Setup()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("WeaponType_int", gunBlueprint.gunType);  
        magazine = gunBlueprint.magazine;
        aim.localPosition = new Vector3(0.0f, 0.0f, gunBlueprint.fireRange);
    }

}
