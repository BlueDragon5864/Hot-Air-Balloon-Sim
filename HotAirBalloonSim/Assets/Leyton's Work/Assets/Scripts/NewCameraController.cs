using UnityEngine;
using System.Collections.Generic;

public class FirstPersonCameraController : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject Flame;
    public GameObject Envelope;
    public Transform player; // Reference to the player's transform
    public float mouseSensitivity = 2f; // Sensitivity of the mouse movement
    public float verticalLookLimit = 80f; // Limit for vertical look
    public float shootForce = 10f;

    public GameObject balloon;
    public List<GameObject> ballasts;
    private BalloonController balloonController;
    private List<BallastController> ballastControllers;

    public float farAway = 3f;
    public float headHeight = 5;

    private float yaw = 0f; // Yaw rotation
    private float pitch = 0f; // Pitch rotation

    void Start()
    {
        // currentTarget = balloon;
        balloonController = balloon.GetComponent<BalloonController>();

        // foreach (GameObject b in ballasts)
        {
            // ballastControllers.Add(b.GetComponent<BallastController>());
        }

        // Make sure the player is assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player == null)
            {
                Debug.LogError("FirstPersonCameraController: Could not find a game object with the 'Player' tag. Please assign the player transform manually.");
                enabled = false;
            }
        }

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
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
        pitch = Mathf.Clamp(pitch, -verticalLookLimit, verticalLookLimit);

        // Apply the rotation to the camera
        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);

        // Position the camera at the player's position
        transform.position = new Vector3(player.position.x, player.position.y + headHeight, player.position.z);

        if ( Input.GetMouseButtonDown(1) )
        {
            // Instantiate a new sphere object
            GameObject sphere = Instantiate(bombPrefab, transform.position, transform.rotation);

            // Add a force to the sphere to make it move
            sphere.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce, ForceMode.Impulse);
        }

        CheckForInteraction();
    }

    void CheckForInteraction()
    {
        // Cast a ray from the camera to detect if the player is looking at the flame or ballasts
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.forward * 0.5f, transform.forward * 5f, out hit))
        {
            Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject == Flame || hit.collider.gameObject == Envelope)
            {
                // Player is looking at the flame
                if (Input.GetMouseButtonDown(0))
                {
                    // Increase the flame's intensity
                    balloonController.UpdateFlameIntensity();
                }
            }
            else
            {
                // Check if the player is looking at a ballast
                for (int i = 0; i < ballasts.Count; i++)
                {
                    if (hit.collider.gameObject == ballasts[i])
                    {
                        // Player is looking at a ballast
                        if (Input.GetMouseButtonDown(0))
                        {
                            // Add ballast
                            // ballastControllers[i].ToggleBallast();
                        }
                        break;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + transform.forward, transform.forward * 5f);
    }
}
