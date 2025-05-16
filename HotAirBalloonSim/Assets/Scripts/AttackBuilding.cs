using UnityEngine;

public class AttackBuilding : Building
{
    public GameObject Weapon;
    public Transform SpawnPoint;
    public GameObject ProjectilePreFab;
    
    public float fireForce;
    public float fireRate;
    public AudioSource firesound;
    public Transform projectileParent;

   

    bool inRange = false;
    Transform enemy;
    float reloadDelay = 0;
    public void Start()
    {
        projectileParent = GameObject.Find("Projectiles").transform;
    }




    // Update is called once per frame
    void FixedUpdate()
    {
        if (inRange && enemy != null && !destroyed) {
            reloadDelay++;
            Weapon.transform.LookAt(enemy);
            //Weapon.transform.Rotate(CalculateLaunchAngle(enemy.transform.position - Weapon.transform.position, enemy.GetComponent<Rigidbody>().linearVelocity),Space.Self);
            //Vector3 newRotation = CalculateLaunchAngle(enemy.transform.position - Weapon.transform.position, enemy.GetComponent<Rigidbody>().linearVelocity);
            
          

            if (reloadDelay > fireRate && !destroyed)
            {
                CreateProjectile();
                animator.Play("Fire");
                firesound.Play();
            }

        }
    }
    public void CreateProjectile() {
        GameObject projectile = Instantiate(ProjectilePreFab.gameObject, new Vector3(SpawnPoint.position.x, SpawnPoint.position.y, SpawnPoint.position.z), Weapon.transform.rotation, projectileParent);
        
        projectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * fireForce, ForceMode.Impulse);
        
        reloadDelay = 0;
        
    }
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Balloon"))
        {
            
            enemy = other.transform;
            inRange = true;
            
            
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Balloon")) {
           
            inRange = false;
            
            
            
        }
    }

    

   

}
