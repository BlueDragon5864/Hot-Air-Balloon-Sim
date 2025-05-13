using UnityEngine;
using UnityEngine.SceneManagement;

// Balloon Script
public class BalloonController : MonoBehaviour
{
    
    public float flameIntensity = 0.5f;
    public float forceStrength = 6;
    public float fanForce;
    public float depletionRate = -9;
    public float gravity = -0.5f;
    public float terminalVelocity;
    public float balloonEfficiency = 1f;

    public Rigidbody rb;

    public HeatBar heatBar;

    void Start()
    {
        // Find all ballast GameObjects and get their BallastController components

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // rb.AddForce(0f, flameIntensity * Mathf.Exp(forceStrength), 0f);
        rb.AddForce(0f, -1f * gravity * Mathf.Exp(forceStrength), 0f);

        if (rb.linearVelocity.y < -1f * terminalVelocity)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, - 1f * terminalVelocity, rb.linearVelocity.z);
        //if (rb.linearVelocity.y > terminalVelocity)
            //rb.linearVelocity = new Vector3(rb.linearVelocity.x, terminalVelocity, rb.linearVelocity.z);

        if ( flameIntensity > 0f ) flameIntensity -= Mathf.Exp(depletionRate);
        heatBar.SetHealth(flameIntensity);
        balloonEfficiency = (-7f - depletionRate) / 2f;

        if (flameIntensity <= 0f) SceneManager.LoadScene("DieScreen");

        if (transform.position.y > 650f) rb.linearVelocity = new Vector3(rb.linearVelocity.x, -10f, rb.linearVelocity.z);
        if (transform.position.y < 30f) rb.linearVelocity = new Vector3(rb.linearVelocity.x, 10f, rb.linearVelocity.z);
    }

    public void UpdateFlameIntensity()
    {
        // flameIntensity += (1f - flameIntensity) / 2f;
        if (flameIntensity > 0f) flameIntensity = Mathf.Sqrt(flameIntensity);
        rb.AddForce(0f, flameIntensity * Mathf.Exp(fanForce), 0f, ForceMode.Impulse);
        heatBar.SetHealth(flameIntensity);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (
            // depletionRate < -7f && 
            collision.gameObject.CompareTag("Projectile")
            ) depletionRate += 0.1f;
    }
}