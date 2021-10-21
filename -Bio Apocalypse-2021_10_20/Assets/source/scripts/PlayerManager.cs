using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    public float moveSpeed;
    float h, v;
    Vector3 movement;
    public Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {;
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }

	private void FixedUpdate()
	{
        move();
    }

	public void move() {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // 이동방향 * 속도 * 프레임단위 시간을 곱해서 카메라의 트랜스폼을 이동
        transform.Translate(dir * moveSpeed * Time.deltaTime);

        movement.Set(h, 0.0f, v);
        movement = Camera.main.transform.TransformDirection(movement);

        movement = movement.normalized * moveSpeed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + movement);


        if (transform.position.magnitude / Time.deltaTime == 0)
        {
            animator.SetFloat("Speed_f", 0);
        }
        else {
            animator.SetFloat("Speed_f", moveSpeed);
		}
    }
}
