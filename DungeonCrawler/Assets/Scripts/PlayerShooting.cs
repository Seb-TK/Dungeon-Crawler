using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //WILL BE CHANGED LATER when modular guns are added
    public GameObject Bullet;
    public float fireRate;
    private float nextFireTime;

    void FixedUpdate()
    {
        if(fireRate == -1)
        {
            this.enabled = false;
        } else if(Input.GetMouseButtonDown(0)){
            if (Time.time > nextFireTime){
                GameObject SpawnedBullet = Instantiate(Bullet, transform.position, transform.rotation);
                SpawnedBullet.GetComponent<Bullet>().isPlayerBullet = true;
                nextFireTime = Time.time + fireRate;
            }
        }
    }
}
