using UnityEngine;

public class Crossbow : MonoBehaviour
{
    public float gravityStrength; // Gravity strength
    public float initialVelocity;

    public Vector3 CalculateLaunchAngle(Vector3 targetPosition, Vector3 targetVelocity)
    {
        float x = targetPosition.x;
        float y = targetPosition.y;
        float z = targetPosition.z;
        float theta = Mathf.Atan(y / Mathf.Sqrt(x * x + z * z));
        float t = 0.0f;
        for ( int i = 0; i < 10; ++i )
        {
            float a = 0.5f * gravityStrength;
            float b = initialVelocity * Mathf.Sin(theta) - targetVelocity.y;
            float c = y;

            try
            {
                t = Mathf.Min
                    (
                    (b + Mathf.Sqrt(b * b - 4.0f * a * c)) / (2 * a),
                    (b - Mathf.Sqrt(b * b - 4.0f * a * c)) / (2 * a)
                    );
            }
            catch
            {
                return Vector3.zero;
            }
            

            theta = Mathf.Asin((y + targetVelocity.y * t + 0.5f * gravityStrength * t * t));
        }
        float phi = Mathf.Atan((z + targetVelocity.z * t) / (x + targetVelocity.x * t));

        return new Vector3(theta, phi, 0);
    }

    void Update()
    {

    }
}
