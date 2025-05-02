using UnityEngine;

public class Bomb : MonoBehaviour
{
    public ParticleSystem[] explosionEffects;
    public ParticleSystem fuse;

    private bool hasExploded = false;

    void Start()
    {
        // Optionally, trigger the explosion immediately
       
    }

    public void Explode()
    {
        if (hasExploded) return;
        GetComponent<MeshRenderer>().enabled = false;
        fuse.Stop();
        hasExploded = true;

        foreach (var effect in explosionEffects)
        {
            if (effect != null)
                effect.Play();
        }

        StartCoroutine(WaitForParticlesToFinish());
    }

    private System.Collections.IEnumerator WaitForParticlesToFinish()
    {
        bool isAnyAlive;
        do
        {
            isAnyAlive = false;
            foreach (var effect in explosionEffects)
            {
                if (effect != null && effect.IsAlive(true))
                {
                    isAnyAlive = true;
                    break;
                }
            }
            yield return null;
        } while (isAnyAlive);

        Destroy(gameObject);
    }
}