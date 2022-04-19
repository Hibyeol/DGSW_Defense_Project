using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_PlayerCamController : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform VRCamera;
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
            float mouseX = VRCamera.rotation.x * mouseSensitivity * Time.deltaTime;
            float mouseY = VRCamera.rotation.y * mouseSensitivity * Time.deltaTime;


            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -75, 50);

            //transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f); // 카메라 각도 
            //playerBody.Rotate(Vector3.up, transform.localRotation.y * mouseSensitivity * Time.deltaTime);
            playerBody.Rotate(Vector3.up, transform.localRotation.y);
            //Debug.Log(transform.localRotation.y);
        }
    }


    void Setup()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
