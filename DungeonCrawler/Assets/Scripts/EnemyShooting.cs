using System.Collections;
using UnityEditor.Search;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    private float nextFireTime;
    [SerializeField] public float fireRate;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > nextFireTime){
            GameObject SpawnedBullet = Instantiate(Bullet, transform.position, transform.rotation);
            SpawnedBullet.GetComponent<Bullet>().isPlayerBullet = false;
            nextFireTime = Time.time + fireRate;
        }
        
    }
}
