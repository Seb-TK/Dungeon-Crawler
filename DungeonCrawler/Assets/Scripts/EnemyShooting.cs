using System.Collections;
using UnityEditor.Search;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    //WILL BE CHANGED LATER when modular guns are added
    public GameObject Bullet;
    public float fireRate;
    public float bulletSpeed;

    private float nextFireTime;

    void FixedUpdate()
    {
        if(fireRate == -1)
        {
            this.enabled = false;
        }
        if (Time.time > nextFireTime){
            GameObject SpawnedBullet = Instantiate(Bullet, transform.position, transform.rotation);
            SpawnedBullet.GetComponent<Bullet>().isPlayerBullet = false;
            SpawnedBullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
            nextFireTime = Time.time + fireRate;
        }
        
    }
}
