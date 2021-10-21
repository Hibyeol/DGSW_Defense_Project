using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

	void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(this.gameObject);
    }
}
