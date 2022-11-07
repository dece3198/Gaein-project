using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float Sensitivity = 1000f;
    [SerializeField] private Transform PlayerBody;
    [SerializeField] private float YRotation = 90f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        YRotation -= mouseY;
        YRotation = Mathf.Clamp(YRotation, -60f, 45f);
        transform.localRotation = Quaternion.Euler(YRotation, 0, 0);
        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
