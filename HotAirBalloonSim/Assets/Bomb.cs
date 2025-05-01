using UnityEngine;

public class Bomb : MonoBehaviour
{
    public ParticleSystem[] particleSystems;



    public void Explode() {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        foreach (ParticleSystem particle in particleSystems) {
            particle.Play();
        }
        while (true) { 
            if (!particleSystems[0].IsAlive()) {
            Destroy(gameObject);
        }
    }
    }
}
