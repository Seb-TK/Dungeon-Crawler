using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Hitbox : MonoBehaviour
{
    private GameObject playerManager;
    private PlayerManager playerManagerScript;
    private float Defense;
    private float DefenseMultiplier;
    
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager");
        playerManagerScript = playerManager.GetComponent<PlayerManager>();

        Defense = playerManagerScript.Defense;

        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bullet") || collider.CompareTag("Melee"))
        {
            
            Bullet BulletScript = collider.gameObject.GetComponent<Bullet>();
            

            if (BulletScript.isPlayerBullet) {

                Part Parent = transform.parent.GetComponent<Part>();

                GameObject visualMesh = null;

                foreach(Transform child in Parent.transform)
                {
                    if (child.CompareTag("VisualMesh"))
                    {
                        visualMesh = child.gameObject;
                    }
                }
                if (Parent.Health == 0)
                {
                    Parent.isBroken = true;
                    playerManagerScript.Health -= 1;
                    Debug.Log("Player Health: " + playerManagerScript.Health);
                    Debug.Log("Part health: " + Parent.Health);
                    
                    visualMesh.GetComponent<Renderer>().material.color = Color.red;

                }
                else
                {
                    playerManagerScript.Health -= 1 * DefenseMultiplier;
                    Parent.Health -= 1;
                    Debug.Log("Player Health: " + playerManagerScript.Health);
                    Debug.Log("Part health: " + Parent.Health);
                }

                if (collider.CompareTag("Bullet"))
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }

    void Update()
    {
        DefenseMultiplier = Defense / 100;
    }
}
