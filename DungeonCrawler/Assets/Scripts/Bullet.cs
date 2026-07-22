using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [HideInInspector] public float bulletSpeed;
    [HideInInspector] public bool isPlayerBullet;
    [HideInInspector] public GameObject Player;

    // Update is called once per frame
    
    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }
    
    void OnTriggerEnter(Collider other)
    {
        // //remember to change it so that you can deal damage to each part
        if (other.CompareTag("Enemy") & isPlayerBullet)
        {
            Destroy(gameObject);
        } 
        if (other.CompareTag("Obs"))
        {
            Destroy(gameObject);
        }
    }
}
