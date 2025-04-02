using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PilotController : MonoBehaviour
{
    public float moveSpeed = 5f;
    // public float jumpForce = 10f;
    public float gravity = 20f;
    public bool superFastMode = false;
    public float maxVelocity = 10f; // Maximum velocity of the ball
    public bool canFly = false;
    public float lowBoundary = 0.0f;
    public Transform respawn;
    /*
    public AudioClip jumpAudioSource;
    public AudioClip deathAudioSource;

    private AudioSource source;
    // Variable to keep track of collected "PickUp" objects.
    private int count;


    // UI text component to display count of "PickUp" objects collected.
    // public TextMeshProUGUI countText;

    // UI object to display winning text.
    // public GameObject winTextObject;
    */
    private Rigidbody rb;
    // private bool isGrounded;
    private Transform cameraTransform;
    private Vector3 cameraForward;
    private Vector3 cameraRight;
    private float horizontal;
    private float vertical;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        /*
        // Initialize count to zero.
        count = 0;

        // Update the count display.
        SetCountText();

        // Initially set the win text to be inactive.
        winTextObject.SetActive(false);

        source = GetComponent<AudioSource>();
        */
    }

    void FixedUpdate()
    {
        

        Vector3 movement = (cameraForward * vertical) + (cameraRight * horizontal);

        // Move the ball
        rb.AddForce(movement * moveSpeed, ForceMode.Impulse);

        // Limit the ball's velocity
        if (!superFastMode) LimitVelocity();

        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        if(this.transform.position.y < lowBoundary)
        {
            this.transform.position = respawn.position;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            // source.PlayOneShot(deathAudioSource);
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
        /*
        // Check if the ball is grounded
        if (canFly) isGrounded = true;
        else isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        // Jump if the space key is pressed and the ball is grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Calculate the jump direction based on the camera's orientation
            Vector3 jumpDirection = (cameraForward * vertical) + (cameraRight * horizontal) + Vector3.up;
            jumpDirection.Normalize();

            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);

            source.PlayOneShot(jumpAudioSource);
        }
        */

        // Apply gravity
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }

    private void LimitVelocity()
    {
        Vector3 velocity = rb.linearVelocity;
        float verticalVelocity = velocity.y;
        float horizontalVelocity = new Vector3(velocity.x, 0f, velocity.z).magnitude;
        velocity.y = 0.0f;
        if (horizontalVelocity > maxVelocity)
        {
            velocity *= (maxVelocity / horizontalVelocity);
            velocity.y = verticalVelocity;
            rb.linearVelocity = velocity;
        }
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

            // Increment the count of "PickUp" objects collected.
            count = count + 1;

            // Update the count display.
            SetCountText();
        }


    }
    */

    /*
    // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Count: " + count.ToString() + "/10";

        // Check if the count has reached or exceeded the win condition.
        if (count >= 10)
        {
            // Display the win text.
            SceneManager.LoadScene("End");
        }
    }
    */
}
