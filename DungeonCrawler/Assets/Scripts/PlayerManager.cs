using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    [Header(" -- Data --")]
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float driftMultiplier;
    [SerializeField] float nitroMultiplier;
    [SerializeField] float maxNitro;
    [SerializeField] float nitroUseSpeed;
    [SerializeField] float nitroRegenSpeed;
    //using nitro can make you heavier so you push harder 
    // (also do more melee damage?)
    [SerializeField] float nitroMassMultiplier;
    [SerializeField] float playerMass;
    [SerializeField] float nitroFatigueThreshold;
    [SerializeField] float nitroFatigueMultiplier;
    
    [Header("Health")]
    public float Health;
    public float DefenseMultiplier;

    [Header("Objects")]
    [SerializeField] GameObject playerObj;
    [SerializeField] GameObject playerMesh;
    [SerializeField] GameObject playerGun;
    [SerializeField] GameObject hullObj;
    [SerializeField] GameObject partObj;
    
    [Header("Scripts")]
    PlayerMovement player;
    
    void Start()
    {
        SpawnCharacter();
    }

    void SpawnCharacter()
    {
        //instantiations
        
        //player
        playerObj = Instantiate(playerObj, new Vector3(0,0,0), Quaternion.identity);
        //hull ---> guns parts
        hullObj = Instantiate(hullObj, new Vector3(0,0,0), Quaternion.identity, playerObj.transform);
        //parts, guns loop through all children
        foreach (Transform child in hullObj.transform)
        {
            if (child.CompareTag("Gun"))
            {
                Instantiate(playerGun, child.position, child.rotation, child);
            }
            else
            {
                GameObject partObjTemp = Instantiate(partObj, child.position, child.rotation, child);
                partObjTemp.transform.localScale = child.localScale;
            }
        }
        GameObject playerMeshTemp = Instantiate(playerMesh, new Vector3(0,0,0), Quaternion.identity, playerObj.transform);
        
        player = playerObj.GetComponent<PlayerMovement>(); 

        player.moveSpeed = moveSpeed;
        player.turnSpeed = turnSpeed;
        player.driftMultiplier = driftMultiplier;
        player.nitroMultiplier = nitroMultiplier;
        player.maxNitro = maxNitro;
        player.nitroUseSpeed = nitroUseSpeed;
        player.nitroRegenSpeed = nitroRegenSpeed;
        //using nitro can make you heavier so you push harder 
        // (also do more melee damage?)
        player.nitroMassMultiplier = nitroMassMultiplier;
        player.playerMass = playerMass;
        player.nitroFatigueThreshold = nitroFatigueThreshold;
        player.nitroFatigueMultiplier = nitroFatigueMultiplier;
        //pp fax
        
    }

    void Update()
    {
        player.moveSpeed = moveSpeed;
        player.turnSpeed = turnSpeed;
        player.driftMultiplier = driftMultiplier;
        player.nitroMultiplier = nitroMultiplier;
        player.maxNitro = maxNitro;
        player.nitroUseSpeed = nitroUseSpeed;
        player.nitroRegenSpeed = nitroRegenSpeed;
        //using nitro can make you heavier so you push harder 
        // (also do more melee damage?)
        player.nitroMassMultiplier = nitroMassMultiplier;
        player.playerMass = playerMass;
        player.nitroFatigueThreshold = nitroFatigueThreshold;
        player.nitroFatigueMultiplier = nitroFatigueMultiplier;
        //pp fax
    }

}
