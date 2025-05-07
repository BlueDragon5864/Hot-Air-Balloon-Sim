using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int resolution = 30; // Number of points in the trajectory
    public float timeStep = 0.1f; // Simulation time step
    public float initialVelocity = 10f; // Initial speed of the throwable
    public float angle = 45f; // Launch angle in degrees

    private void Update()
    {
        DrawTrajectory();
    }

    void DrawTrajectory()
    {
        Vector3 startPos = Camera.main.transform.position;
        Vector3 launchDirection = Quaternion.Euler(-angle, 0, 0) * Camera.main.transform.forward;
        Vector3 velocity = launchDirection * initialVelocity;
        float gravity = Physics.gravity.y;

        Vector3[] points = new Vector3[resolution];
        for (int i = 0; i < resolution; i++)
        {
            float time = i * timeStep;
            points[i] = startPos + velocity * time + 0.5f * new Vector3(0, gravity, 0) * time * time;
        }

        lineRenderer.positionCount = resolution;
        lineRenderer.SetPositions(points);
    }
}
