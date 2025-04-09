using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PilotController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = 1f;
    public bool superFastMode = false;
    public float maxVelocity = 10f; 
    public bool canFly = false;
    public float lowBoundary = 0.0f;
    public Transform respawn;
    
    private Rigidbody rb;
    private Transform cameraTransform;
    private Vector3 cameraForward;
    private Vector3 cameraRight;
    private float horizontal;
    private float vertical;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
      }

    void FixedUpdate()
    {
        Vector3 movement = (cameraForward * vertical) + (cameraRight * horizontal);
        
        rb.AddForce(movement * moveSpeed, ForceMode.Impulse);
        rb.AddForce(new Vector3(0, -1f * gravity, 0));
        
        if(this.transform.position.y < lowBoundary)
        {
            this.transform.position = respawn.position;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            
        }
    }

    void Update()
    {
        // Get input from arrow keys
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the camera's orientation
        cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        cameraRight = cameraTransform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();
        
    }
    
}
