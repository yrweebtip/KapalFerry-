using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 4f;
    public float turnSmoothTime = 0.1f;

    private CharacterController characterController;
    private Joystick joystick;
    private GameObject cam;
    private float turnSmoothVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        joystick = Object.FindFirstObjectByType<Joystick>();
        cam = Camera.main.gameObject;
    }

    void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;

        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
        }
    }
}
