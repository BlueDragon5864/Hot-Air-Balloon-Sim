using UnityEngine;

public class AttackBuilding : Building
{
    public GameObject Weapon;
    public Transform SpawnPoint;
    public GameObject ProjectilePreFab;
    
    public float fireForce;
    public float fireRate;

    public Transform projectileParent;

   

    bool inRange = false;
    Transform enemy;
    float reloadDelay;
    public void Start()
    {
        
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
            
            reloadDelay = 0;
            
        }
    }

    float gravityStrength = -9.81f; // Gravity strength
    

    public Vector3 CalculateLaunchAngle(Vector3 targetPosition, Vector3 targetVelocity)
    {
        float x = targetPosition.x;
        float y = targetPosition.y;
        float z = targetPosition.z;
        float theta = Mathf.Atan(y / Mathf.Sqrt(x * x + z * z));
        float t = 0.0f;
        for (int i = 0; i < 10; ++i)
        {
            float a = 0.5f * gravityStrength;
            float b = fireForce * Mathf.Sin(theta) - targetVelocity.y;
            float c = y;

            try
            {
                t = Mathf.Max
                    (
                    (b + Mathf.Sqrt(b * b - 4.0f * a * c)) / (2 * a),
                    (b - Mathf.Sqrt(b * b - 4.0f * a * c)) / (2 * a)
                    );
                Debug.Log("t: " + t);
            }
            catch
            {
                Debug.Log("my t is squirting");
                return Vector3.zero;
            }

            Debug.Log("t" + t + "x " + x + "y" + y + "z" + z);
            theta = Mathf.Asin((y + targetVelocity.y * t + 0.5f * gravityStrength * t * t));
        }
        float phi = Mathf.Atan((z + targetVelocity.z * t) / (x + targetVelocity.x * t));
        Debug.Log(theta + "" + phi);

        return new Vector3(theta, phi, 0);
    }

   

}
