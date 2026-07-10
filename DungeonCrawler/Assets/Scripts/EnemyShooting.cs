using System.Collections;
using UnityEditor.Search;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject Bullet;
    public float fireRate;

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
            nextFireTime = Time.time + fireRate;
        }
        
    }
}
