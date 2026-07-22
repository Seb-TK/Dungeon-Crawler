using System.Collections;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyShooting : MonoBehaviour
{
    //WILL BE CHANGED LATER when modular guns are added
    public GameObject Bullet;
    public float fireRate;
    public float bulletSpeed;
    private float nextFireTime;
    private Transform playerPos;

    void Start()
    {

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        if(fireRate == -1)
        {
            this.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (Time.time > nextFireTime){

            GameObject SpawnedBullet = Instantiate(Bullet, transform.position, transform.rotation);
            SpawnedBullet.GetComponent<Bullet>().isPlayerBullet = false;
            SpawnedBullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
            
            nextFireTime = Time.time + fireRate;
        }

        transform.LookAt(playerPos);
    }
}
