using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerShooting : MonoBehaviour
{
    //WILL BE CHANGED LATER when modular guns are added
    public GameObject Bullet;
    public float fireRate;
    public float bulletSpeed;
    private float nextFireTime;
    public float TurnSpeed;
    public string AimType;
    bool firing;

    void Update()
    {
        if(fireRate == -1)
        {
            this.enabled = false;
        } else if(firing){
            if (Time.time > nextFireTime){
                GameObject SpawnedBullet = Instantiate(Bullet, transform.position, transform.rotation);
                SpawnedBullet.GetComponent<Bullet>().isPlayerBullet = true;
                nextFireTime = Time.time + fireRate;
            }
        }
        if (AimType == "Automatic")
        { 
            Collider closestEnemy = findClosestEnemy();
            
            if (closestEnemy != null)
            {

                Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
                Quaternion targetDirection = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, TurnSpeed * Time.deltaTime);
            }
        } else if(AimType == "Manual")
        {
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (firing){firing = false;} else {firing = true;}
        }

    }

    Collider findClosestEnemy()
    {
        LayerMask mask = LayerMask.GetMask("Enemy");
        Collider[] collisions = Physics.OverlapSphere(transform.position, Mathf.Infinity, mask);
        
        float smallestDistance = Mathf.Infinity;
        Collider closestEnemy = null;

        foreach (Collider enemy in collisions)
        {
            
            Vector3 enemyLocation = enemy.transform.position;
            float distance = Vector3.Distance(enemyLocation, transform.position);
            
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                closestEnemy = enemy;
            }
            
        }
        return closestEnemy;
    }
}