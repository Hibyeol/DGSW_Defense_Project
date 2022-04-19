using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamController_kd : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;

    float xRotation = 0.0f;

	private void Start()
	{
        Setup();

	}

	void Update()
    {
        if (GameManager.instance.isPmove == true)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -75, 50);

            transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
            playerBody.Rotate(Vector3.up, mouseX);
        }
    }
    

    void Setup() {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
