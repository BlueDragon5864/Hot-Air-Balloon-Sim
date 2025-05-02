using UnityEngine;

public class Building : MonoBehaviour
{
    public float health;
    public bool destroyed;

    public Animator animator;

    public AudioSource source;
    public AudioClip clip;  

    private void Start()
    {
        
        source = gameObject.GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.clip = clip;
    }
    // Update is called once per frame
    void Update()
    {
        if (!destroyed && health <= 0) {
            destroyed = true;
            animator.Play("Destruction");
            source.Play();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {

         
        if (!destroyed && collision.gameObject.CompareTag("Bomb") ) {

            collision.gameObject.GetComponent<Bomb>().Explode();
            health -= 25;
        }
    }
}
