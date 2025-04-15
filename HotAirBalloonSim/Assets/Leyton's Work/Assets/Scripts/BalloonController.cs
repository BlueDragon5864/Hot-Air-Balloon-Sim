using UnityEngine;

// Balloon Script
public class BalloonController : MonoBehaviour
{
    public float maxAltitude, minAltitude;
    public float xSpeed, zSpeed;
    //public float speed = 0.0001f;
    public float flameIntensity = 0.5f;
    public float forceStrength;
    public float depletionRate;
    public float gravity = -0.5f;

    private Rigidbody rb;

    void Start()
    {
        // Find all ballast GameObjects and get their BallastController components

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(0f, flameIntensity * Mathf.Exp(forceStrength), 0f);
        rb.AddForce(0f, -1f * gravity * Mathf.Exp(forceStrength), 0f);

        if ( flameIntensity > 0f ) flameIntensity -= Mathf.Exp(depletionRate);
        Debug.Log(rb.linearVelocity);
    }

    public void UpdateFlameIntensity()
    {
        flameIntensity += (1f - flameIntensity) / 2f;
    }
}