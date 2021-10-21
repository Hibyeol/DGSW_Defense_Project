using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    float mx, my;
    float rx, ry;

    private float rotSpeed = 125;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update() {
        Rotate();
	}

    public void Rotate() {
        mx = rotSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;
        rx = Mathf.Clamp(rx + mx, -45, 80);

        my = rotSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        ry = transform.eulerAngles.y + my;

        transform.eulerAngles = new Vector3(-rx, ry, 0.0f);
    }
}
