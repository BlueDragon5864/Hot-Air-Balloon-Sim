using UnityEngine;

public class Building : MonoBehaviour
{
    public float health;
    public bool destroyed;

    public Animator animator;
    

    // Update is called once per frame
    void Update()
    {
        if (!destroyed && health <= 0) {
            destroyed = true;
            animator.Play("Destruction");
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
