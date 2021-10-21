using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{

    public GunBlueprint gunBlueprint;
    public GameObject firePoint;
    RaycastHit hit;

    private Animator animator;

    bool ifClick = false;

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
    }

    void Fire()
    {
        animator.SetBool("Shoot_b", ifClick);
        
        if (Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, gunBlueprint.fireRange))
        {
            Debug.Log("hit point : " + hit.point + ", distance : " + hit.distance + ", name : " + hit.collider.name);
            Debug.DrawRay(firePoint.transform.position, firePoint.transform.forward * gunBlueprint.fireRange, Color.red);

        }
        else
        {
            Debug.DrawRay(firePoint.transform.position, firePoint.transform.TransformDirection(Vector3.forward) * 1000f, Color.red);

        }
        
    }

    void Setup()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("WeaponType_int", gunBlueprint.gunType);
        magazine = gunBlueprint.magazine;
    }

}
