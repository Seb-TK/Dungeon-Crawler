using UnityEngine;
//Yoda is a retard
public class Part : MonoBehaviour
{
    //part script, holds all the data for the part
    // has children that are the hitbox and swappable visual mesh
    
    
    // hitbox will change health values until its broken
    // once broken it will damage main health pool and apply effect
    public float Health;
    public float DefenseMultiplier;
    public bool isBroken;
    public string BrokenEffect;
    private GameObject playerManager;
    private PlayerManager playerManagerScript;

    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager");
        playerManagerScript = playerManager.GetComponent<PlayerManager>();
    }
    void Update()
    {
        
    }
}
