using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float speed = 4;

    void Update()
    {
        float dt = Time.deltaTime;
        float xDir = 0.0f;
        float zDir = 0.0f;

        if (Keyboard.current.dKey.isPressed)
        {
            xDir = 2.0f;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            xDir = -2.0f;
        }

        if (Keyboard.current.wKey.isPressed)
        {
            zDir = 2.0f;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            zDir = -2.0f;
        }

        float dz = zDir * speed * dt;
        transform.position = transform.position + new Vector3(0.0f, 0.0f, dz);

        float dx = xDir * speed * dt;
        transform.position = transform.position + new Vector3(dx, 0.0f, 0.0f);
    }
}













//using UnityEngine;
//using UnityEngine.InputSystem;
//
//public class PlayerMovement : MonoBehaviour
//{
//    [Header("Movement Settings")]
//    [SerializeField] private float moveSpeed = 5f;
//    [SerializeField] private float rotationSpeed = 10f;
//
//    [Header("References")]
//    [SerializeField] private CharacterController controller;
//
//    private Vector2 moveInput;
//    private Vector3 moveDirection;
//
//    void Start()
//    {
//        // Get CharacterController if not assigned
//        if (controller == null)
//        {
//            controller = GetComponent<CharacterController>();
//        }
//    }
//
//    void Update()
//    {
//        HandleMovement();
//        HandleRotation();
//    }
//
//    void HandleMovement()
//    {
//        // Get input from new Input System
//        moveInput = new Vector2(
//            Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ? 1 : Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ? -1 : 0,
//            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ? 1 : Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ? -1 : 0
//        );
//
//        // Calculate movement direction relative to world space
//        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
//
//        // Apply gravity
//        if (!controller.isGrounded)
//        {
//            moveDirection.y -= 9.81f * Time.deltaTime;
//        }
//        else
//        {
//            moveDirection.y = -0.5f; // Small value to keep grounded
//        }
//
//        // Move the character
//        Vector3 movement = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z) * moveSpeed * Time.deltaTime;
//        controller.Move(movement);
//    }
//
//    void HandleRotation()
//    {
//        // Only rotate if there's movement input
//        if (moveInput.magnitude >= 0.1f)
//        {
//            // Calculate target rotation based on movement direction
//            Vector3 lookDirection = new Vector3(moveInput.x, 0f, moveInput.y);
//            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
//
//            // Smoothly rotate towards movement direction
//            transform.rotation = Quaternion.Slerp(
//                transform.rotation,
//                targetRotation,
//                rotationSpeed * Time.deltaTime
//            );
//        }
//    }
//}