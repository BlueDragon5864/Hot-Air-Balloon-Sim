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
    public float shootForce;
    private float charge = 0;
    public float maxCharge = 20f;
    private bool charging = false;
    public int chargeMetronome = 100;
    public float chargeIncreaseRate;
    public int throwDelay;
    private int delay;
    public Animator handBomb;
    public Animator leaf;
    private int count = 0;


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

    void FixedUpdate()
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

        if (Input.GetMouseButton(1) && delay == 0)
        {
            
            if (charge < 1) {
                handBomb.Play("ChargeBomb");
                charge = 1;
            }
            
            
            if (count % chargeMetronome == 0 && count > 0)
            {
                /*
                charge += shootForce;
                if (charge > maxCharge) {
                    charge = maxCharge;
                }
                */
                charge = Mathf.Pow((charge + 1f) / maxCharge, chargeIncreaseRate) * maxCharge;
                Debug.Log("Charge: " + charge);
            }
            
       
            count++;
            charging = true;
            
        }
        else if (charging)
        {
            // Instantiate a new sphere object
            GameObject sphere = Instantiate(bombPrefab, transform.position, transform.rotation);

            //set bomb velocity the balloon velocity
            sphere.GetComponent<Rigidbody>().linearVelocity = balloon.GetComponent<Rigidbody>().linearVelocity;

            // Add a force to the sphere to make it move
            sphere.GetComponent<Rigidbody>().AddForce(transform.forward * (shootForce + charge), ForceMode.Impulse);

            charge = 0;
            count = 0;
            charging = false;
            handBomb.Play("Idle");
            delay = throwDelay;
        }
        else
        {
            if (delay > 0) delay--;
        }
        
        CheckForInteraction();
    }

    void CheckForInteraction()
    {
        // Cast a ray from the camera to detect if the player is looking at the flame or ballasts
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.forward * 0.5f, transform.forward * 5f, out hit))
        {
            
            if (hit.collider.gameObject == Flame || hit.collider.gameObject == Envelope)
            {
                // Player is looking at the flame
                if (Input.GetMouseButtonDown(0))
                {
                    // Increase the flame's intensity
                    balloonController.UpdateFlameIntensity();
                    leaf.Play("fan");
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
