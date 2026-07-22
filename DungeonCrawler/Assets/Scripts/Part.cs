using UnityEngine;
//Yoda is a retard
public class Part : MonoBehaviour
{
    //part script, holds all the data for the part
    // has children that are the hitbox and swappable visual mesh
    
    
    // hitbox will change health values until its broken
    // once broken it will damage main health pool and apply effect
    public float Health;
    public bool isBroken;
    public string BrokenEffect;
    private GameObject playerManager;
    private PlayerManager playerManagerScript;
    [SerializeField]private float PartDefenseMultiplier;
    private float OverallDefenseMultiplier;
    
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager");
        playerManagerScript = playerManager.GetComponent<PlayerManager>();

        OverallDefenseMultiplier = playerManagerScript.DefenseMultiplier;
        
    }

    void Update()
    {
        PartDefenseMultiplier = 1;
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bullet") || collider.CompareTag("Melee"))
        {
            
            Bullet BulletScript = collider.gameObject.GetComponent<Bullet>();
            //melee script here
            //use a break statement

            if (BulletScript.isPlayerBullet == false) {

                Transform visualMesh = transform.GetChild(0);

                if (Health == 0)
                {
                    isBroken = true;
                    playerManagerScript.Health -= 1;

                    visualMesh.GetComponent<Renderer>().material.color = Color.red;

                }
                else
                {
                    playerManagerScript.Health -= 1 * PartDefenseMultiplier * OverallDefenseMultiplier;
                    Health -= 1;
                }

                if (collider.CompareTag("Bullet"))
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }
    
}
