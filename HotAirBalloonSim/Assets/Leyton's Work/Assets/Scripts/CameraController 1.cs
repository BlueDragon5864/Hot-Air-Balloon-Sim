using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the ball's transform
    public float distance = 50f; // Distance of the camera from the ball
    public float height = 2f; // Height of the camera above the ball
    public float mouseSensitivity = 2f; // Sensitivity of the mouse movement
    public float cameraCollisionOffset = 0.2f; // Offset to prevent the camera from clipping through objects
    public LayerMask collisionLayers; // Layers that the camera should collide with

    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        // Make sure the target is assigned
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            if (target == null)
            {
                Debug.LogError("CameraController: Could not find a game object with the 'Ball' tag. Please assign the target transform manually.");
                enabled = false;
            }
        }
    }

    void Update()
    {
        // Get the mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Update the yaw and pitch based on the mouse input
        yaw += mouseX;
        pitch -= mouseY;

        // Clamp the pitch to prevent the camera from going too high or low
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // Calculate the camera's position based on the target's position, distance, and height
        Vector3 desiredCameraPosition = target.position - Quaternion.Euler(pitch, yaw, 0f) * new Vector3(0f, height, -distance);

        // Check for collisions between the camera and the target
        RaycastHit hit;
        if (Physics.Raycast(target.position, desiredCameraPosition - target.position, out hit, distance, collisionLayers))
        {

            // Adjust the camera's position to avoid clipping through objects
            transform.position = hit.point - (desiredCameraPosition - target.position).normalized * cameraCollisionOffset;
        }
        else
        {
            // Set the camera's position and rotation
            transform.position = desiredCameraPosition;
        }

        transform.LookAt(target);
    }
}
