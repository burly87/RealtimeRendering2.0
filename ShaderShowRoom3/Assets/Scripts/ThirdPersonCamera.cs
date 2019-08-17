using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public bool lockCursor;
    public float mouseSensitivity = 10.0f;
    public Transform target;
    public float distanceFromTarget = 2.0f;

    public Vector2 pitchMinMax = new Vector2(-40.0f, 85.0f);        // to clamp Rotation in y

    public float rotationSmoothTime = 0.12f;                        // smoothing out the Camera
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;



    private float yaw;
    private float pitch;

    void Start()
    {
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X");
        pitch -= Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
