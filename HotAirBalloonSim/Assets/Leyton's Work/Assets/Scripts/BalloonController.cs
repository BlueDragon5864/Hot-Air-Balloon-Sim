using UnityEngine;
using System.Collections.Generic;

// Balloon Script
public class BalloonController : MonoBehaviour
{
    public float currentAltitude;
    public float maxAltitude, minAltitude;
    public float ascendRate, descendRate;
    public float xSpeed, zSpeed;
    public float speed = 0.0001f;

    private BallastController[] ballasts;
    private Rigidbody rb;

    void Start()
    {
        // Find all ballast GameObjects and get their BallastController components
        ballasts = FindObjectsByType<BallastController>(FindObjectsSortMode.InstanceID);

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Update the balloon's position and rotation based on the ballast changes
        UpdateBalloonMovement();
    }

    public void UpdateBalloonMovement()
    {
        // Calculate the total mass of the ballasts
        GetTotalBallastMass();

        // Update the balloon's altitude based on the total ballast mass

        // Update the balloon's position and rotation to simulate the movement
        rb.linearVelocity = new Vector3(xSpeed * speed, 0, zSpeed * speed);

        // Update the flame's intensity based on the balloon's ascent/descent rate
        UpdateFlameIntensity();
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

    public GameObject flame;

    void UpdateFlameIntensity()
    {
        // Adjust the flame's scale and particle emission rate based on the balloon's ascent/descent rate
        float flameScale = Mathf.Clamp(Mathf.Abs(currentAltitude - transform.position.y) * 0.1f, 0.5f, 2f);
        flame.transform.localScale = new Vector3(flameScale, flameScale, flameScale);

        var emission = flame.GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = Mathf.Clamp(Mathf.Abs(currentAltitude - transform.position.y) * 10f, 10f, 50f);
    }

    public void IncreaseFlameIntensity()
    {
        // Increase the flame's scale and particle emission rate
        float flameScale = Mathf.Clamp(flame.transform.localScale.x + 0.1f, 0.5f, 2f);
        flame.transform.localScale = new Vector3(flameScale, flameScale, flameScale);

        var emission = flame.GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = Mathf.Clamp(emission.rateOverTime.constant + 5f, 10f, 50f);
    }

    public void DecreaseFlameIntensity()
    {
        // Decrease the flame's scale and particle emission rate
        float flameScale = Mathf.Clamp(flame.transform.localScale.x - 0.1f, 0.5f, 2f);
        flame.transform.localScale = new Vector3(flameScale, flameScale, flameScale);

        var emission = flame.GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = Mathf.Clamp(emission.rateOverTime.constant - 5f, 10f, 50f);
    }
}