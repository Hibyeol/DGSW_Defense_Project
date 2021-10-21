using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamController : MonoBehaviour
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
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75, 50);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up, mouseX);
    }

    /*
    // 사용법은 StartCoroutine(playerCamController.Shake(지속시간, 흔들림 크기));
    public IEnumerator Shake(float duration, float magnitude) // 지속시간, 흔들림 크기
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
		}

        transform.localPosition = originalPos;
	}
    */

    void Setup() {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
