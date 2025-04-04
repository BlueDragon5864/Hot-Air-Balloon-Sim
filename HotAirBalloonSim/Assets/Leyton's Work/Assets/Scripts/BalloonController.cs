using UnityEngine;

// Balloon Script
public class BalloonController : MonoBehaviour
{
    public float maxAltitude, minAltitude;
    public float xSpeed, zSpeed;
    public float speed = 0.0001f;
    public float flameIntensity = 0.5f;

    private BallastController[] ballasts;
    private Rigidbody rb;

    void Start()
    {
        // Find all ballast GameObjects and get their BallastController components
        ballasts = FindObjectsByType<BallastController>(FindObjectsSortMode.InstanceID);

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Update the balloon's position and rotation based on the ballast changes
        UpdateBalloonMovement();
        // if ( flameIntensity > 0f ) flameIntensity -= 0.0001f;
    }

    public void UpdateBalloonMovement()
    {
        // Calculate the total mass of the ballasts
        GetTotalBallastMass();

        // Update the balloon's altitude based on the total ballast mass

        // Update the balloon's position and rotation to simulate the movement
        rb.linearVelocity = new Vector3(xSpeed * speed, (flameIntensity - 0.5f), zSpeed * speed);

    }

    void GetTotalBallastMass()
    {
        foreach (var ballast in ballasts)
        {
            if (ballast.toggled)
            {
                xSpeed += ballast.xEffect;
                zSpeed += ballast.zEffect;
            }
        }
    }
    public void UpdateFlameIntensity()
    {
        flameIntensity += (1f - flameIntensity) / 2f;
    }
}