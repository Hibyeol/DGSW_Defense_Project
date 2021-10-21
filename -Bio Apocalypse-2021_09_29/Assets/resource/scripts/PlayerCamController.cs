using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamController : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;
    //public Transform leftArm;
    //public Transform rightArm;

    float xRotation = 0.0f;

	private void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75, 50);

        //leftArm.Rotate(Vector3.right, transform.rotation.x);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up, mouseX);
    }
}
