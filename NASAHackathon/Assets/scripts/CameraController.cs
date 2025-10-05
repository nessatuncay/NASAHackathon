using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Camera Settings")]
    [SerializeField] private float distance = 10f;
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float minVerticalAngle = 10f;
    [SerializeField] private float maxVerticalAngle = 80f;
    [SerializeField] private float collisionOffset = 0.3f;

    private float currentX = 0f;
    private float currentY = 45f;

    void Start()
    {
        // Lock cursor to center of screen (optional)
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (player == null) return;

        HandleCameraRotation();
        UpdateCameraPosition();
    }

    void HandleCameraRotation()
    {
        // Get mouse movement from new Input System
        Vector2 mouseDelta = Vector2.zero;

        if (Mouse.current != null)
        {
            mouseDelta = Mouse.current.delta.ReadValue();
        }

        // Update rotation angles
        currentX += mouseDelta.x * mouseSensitivity * Time.deltaTime;
        currentY -= mouseDelta.y * mouseSensitivity * Time.deltaTime;

        // Clamp vertical rotation
        currentY = Mathf.Clamp(currentY, minVerticalAngle, maxVerticalAngle);
    }

    void UpdateCameraPosition()
    {
        // Calculate rotation
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // Calculate desired position offset from player
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        Vector3 desiredPosition = player.position + offset;

        // Check for collisions between player and camera
        RaycastHit hit;
        if (Physics.Raycast(player.position, offset.normalized, out hit, distance))
        {
            // If something is in the way, move camera closer
            desiredPosition = player.position + offset.normalized * (hit.distance - collisionOffset);
        }

        // Set camera position and look at player
        transform.position = desiredPosition;
        transform.LookAt(player.position);
    }
}