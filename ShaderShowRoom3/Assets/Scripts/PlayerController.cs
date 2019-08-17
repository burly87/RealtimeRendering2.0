using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 0.7f;
    public float runSpeed = 1.5f;

    public float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    private float speedSmoothVelocity;
    private float currentSpeed;

    // private Animator animator;
    private Transform cameraTrans;

    void Start()
    {
        // animator = GetComponent<Animator>();'
        cameraTrans = Camera.main.transform;
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTrans.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        bool running = Input.GetKey (KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        this.transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        // not needed here without Animation. but for later
        // float animationSpeedPerc = ((running) ? 1: 0.5f) * inputDir.magnitude;
        // animator.SetFloat("speedPercent", animationSpeedPerc, speedSmoothTime, Time*deltaTime);
    }
}
