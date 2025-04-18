using UnityEngine;

// Balloon Script
public class BalloonController : MonoBehaviour
{
    
    public float flameIntensity = 0.5f;
    public float forceStrength = 6;
    public float depletionRate = -9;
    public float gravity = -0.5f;
    public float terminalVelocity;

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

        if (rb.linearVelocity.y < -1f * terminalVelocity)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, - 1f * terminalVelocity, rb.linearVelocity.z);

        if ( flameIntensity > 0f ) flameIntensity -= Mathf.Exp(depletionRate);
        
    }

    public void UpdateFlameIntensity()
    {
        // flameIntensity += (1f - flameIntensity) / 2f;
        if (flameIntensity > 0f) flameIntensity = Mathf.Sqrt(flameIntensity);
    }
}