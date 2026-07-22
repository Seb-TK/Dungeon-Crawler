using UnityEngine;
//justjuice for shit cube
public class EnemyPart : MonoBehaviour
{

    [SerializeField] private float partHealth;
    private bool isBroken;
    private float partDefense;
    private Enemy enemy;
    void Start()
    {
        enemy = transform.parent.parent.parent.gameObject.GetComponent<Enemy>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bullet") || collider.CompareTag("Melee"))
        {
            
            Bullet BulletScript = collider.gameObject.GetComponent<Bullet>();
            //melee script here
            //use a break statement

            if (BulletScript.isPlayerBullet == true) {
                
                Transform visualMesh = null;
                
                if (partHealth == 0)
                {
                    enemy.health -= 1;

                    if(isBroken == false)
                    {
                        visualMesh = transform.GetChild(0);
                        Destroy(visualMesh.gameObject);
                    }
                    isBroken = true;
                }
                else
                {
                    enemy.health -= 1 * partDefense;
                    partHealth -= 1;
                }

                if (collider.CompareTag("Bullet"))
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
