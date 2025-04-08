using UnityEngine;

// Ballast Script
public class BallastController : MonoBehaviour
{
    public bool toggled = false;
    public float xEffect, zEffect;

    public void ToggleBallast()
    {
        toggled = !toggled;
        GetComponent<MeshRenderer>().enabled = toggled;
    }
}