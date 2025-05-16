using UnityEngine;
using System.Collections.Generic;
using System;

public class FirstPersonCameraController : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject Flame;
    public GameObject Envelope;
    //public Transform player; // Reference to the player's transform
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

    public LineRenderer lineRenderer;
    public int resolution = 60; // Number of points in the trajectory
    public float timeStep = 0.1f; // Simulation time step
    public float angle = 0f; // Launch angle in degrees
    public int lineStart = 0;
    public float below = 0;
    public float rightOffset = 2f;

    public AudioSource fan;
    
    void DrawTrajectory(float initialVelocity)
    {
        Vector3 startPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - below, Camera.main.transform.position.z) + Camera.main.transform.right * rightOffset;
        Vector3 launchDirection = Quaternion.Euler(-angle, 0, 0) * Camera.main.transform.forward;
        Vector3 velocity = launchDirection * initialVelocity + balloonController.rb.linearVelocity;
        float gravity = Physics.gravity.y;

        Vector3[] points = new Vector3[resolution];
        for (int i = lineStart; i < resolution; i++)
        {
            float time = i * timeStep;
            points[i] = startPos + velocity * time + 0.5f * new Vector3(0, gravity, 0) * time * time;
        }

        lineRenderer.positionCount = resolution;
        lineRenderer.SetPositions(points);
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;

        Color translucentWhite = new Color(1f, 1f, 1f, 0.5f);
        Color opaqueWhite = new Color(1f, 1f, 1f, 1f);

        lineRenderer.startColor = translucentWhite;
        lineRenderer.endColor = opaqueWhite;

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // currentTarget = balloon;
        balloonController = balloon.GetComponent<BalloonController>();
        
        // foreach (GameObject b in ballasts)
        {
            // ballastControllers.Add(b.GetComponent<BallastController>());
        }

        // Make sure the player is assigned
        //if (player == null)
        {
            //player = GameObject.FindGameObjectWithTag("Player").transform;
            //if (player == null)
            {
                //Debug.LogError("FirstPersonCameraController: Could not find a game object with the 'Player' tag. Please assign the player transform manually.");
                //enabled = false;
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
        //transform.position = new Vector3(player.position.x, player.position.y + headHeight, player.position.z);
        CheckForInteraction();


    }
    void FixedUpdate() { 
        if (Input.GetMouseButton(1) && delay == 0)
        {
            
            if (charge < 1) {
                handBomb.Play("ChargeBomb");
                charge = 1;
            }
            
            
            if (count % chargeMetronome == 0 && count > 0)
            {
                
                charge += shootForce;
                if (charge > maxCharge) {
                    charge = maxCharge;
                }
                
                //charge = Mathf.Pow((charge + 1f) / maxCharge, chargeIncreaseRate) * maxCharge;
                
            }
            
       
            count++;
            charging = true;

            DrawTrajectory(charge + shootForce);
        }
        else if (charging)
        {
            // Instantiate a new sphere object
            GameObject sphere = Instantiate(bombPrefab, transform.position + Camera.main.transform.right * rightOffset, transform.rotation);

            //set bomb velocity the balloon velocity
            sphere.GetComponent<Rigidbody>().linearVelocity = balloon.GetComponent<Rigidbody>().linearVelocity;

            // Add a force to the sphere to make it move
            sphere.GetComponent<Rigidbody>().AddForce(transform.forward * (shootForce + charge), ForceMode.Impulse);

            charge = 0;
            count = 0;
            charging = false;
            handBomb.Play("Idle");
            delay = throwDelay;

            if (lineRenderer != null)
            {
                lineRenderer.positionCount = 0;
            }
        }
        else
        {
            if (delay > 0) delay--;
        }

        

        
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
                    fan.Play();
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
