using JetBrains.Annotations;
using NUnit.Framework.Internal.Commands;
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
    public float Accuracy;
    GameObject visualMesh;
    [SerializeField] AimAssistCrosshair aimAssistCrosshair;
    [SerializeField] GunData Gun;
    GunData logEquippedGun;
    bool firing;
    void Start()
    {
        aimAssistCrosshair = FindAnyObjectByType<AimAssistCrosshair>().GetComponent<AimAssistCrosshair>();
    }

    void Update()
    {
        
        
        if(fireRate != 0)
        {
            if(firing){
                if (Time.time > nextFireTime){
                    GameObject SpawnedBullet = Instantiate(Bullet, transform.position, transform.rotation);
                    SpawnedBullet.GetComponent<Bullet>().isPlayerBullet = true;
                    SpawnedBullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                    nextFireTime = Time.time + fireRate;
                }
            }

            Vector3 direction;
            Quaternion targetDirection;
            if(aimAssistCrosshair.targetedEnemy != null){
                direction = (aimAssistCrosshair.targetedEnemy.transform.position - transform.position).normalized;
                targetDirection = Quaternion.LookRotation(direction);
            }
            else
            {
                targetDirection = Camera.main.transform.rotation;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, TurnSpeed * Time.deltaTime);
            
            if (Input.GetMouseButtonDown(0))
            {
                if (firing){firing = false;} else {firing = true;}
            }
        }
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
    
    //apply scriptable object (gun) if new gun is equipped
        if (Gun != logEquippedGun)
        {
            Bullet = Gun.Bullet;
            fireRate = Gun.FireRate;
            bulletSpeed = Gun.BulletSpeed;
            TurnSpeed = Gun.TurnSpeed;
            if(visualMesh != null)
            {
                Destroy(visualMesh);
            }
            visualMesh = Instantiate(Gun.VisualMesh, transform.position, Quaternion.identity, transform);
            
        }
        logEquippedGun = Gun;

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