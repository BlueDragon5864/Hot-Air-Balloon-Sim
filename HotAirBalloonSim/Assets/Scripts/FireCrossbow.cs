using UnityEngine;

public class FireCrossbow : MonoBehaviour
{
    public GameObject crossbow;
    public Animator animator;

    private void Start()
    {
        animator = crossbow.GetComponent<Animator>();
    }
    public void runFireAnimation() {
        
        animator.Play("Fire");
    }
}
