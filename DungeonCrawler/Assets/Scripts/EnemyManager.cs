using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject enemyHull;
    [SerializeField] private GameObject enemyPart;
    [SerializeField] private GameObject enemyGun;
    
    [SerializeField] private EnemyData[] EnemyList;
    //potential spawn locations (temp for testing)
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;
    
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SpawnEnemyAtRandomPoint(0);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SpawnEnemyAtRandomPoint(1);
        }
    }
    void SpawnEnemyAtRandomPoint(int EnemyType)
    {
        // probably change later to spawn from gates or something
        Vector3 SpawnPosition = new Vector3
        (
            Random.Range(minX, maxX),
            0,
            Random.Range(minZ, maxZ)
        );
        SpawnEnemy(EnemyType,SpawnPosition);
    }

    void SpawnEnemy(int EnemyIndex, Vector3 spawnPosition)
    {
        // picks out which scriptable object (stats for enemey)
        EnemyData EnemyType = EnemyList[EnemyIndex];
        // spawns the enemy at the spot
        GameObject SpawnedEnemy = Instantiate(Enemy, spawnPosition, Quaternion.identity);
        GameObject SpawnedEnemyHull = Instantiate(enemyHull, spawnPosition, Quaternion.identity, SpawnedEnemy.transform);

        foreach (Transform child in SpawnedEnemyHull.transform)
        {
            if (child.CompareTag("Gun"))
            {
                Instantiate(enemyGun, child.position, child.rotation, child);
            }
            else
            {
                GameObject partObjTemp = Instantiate(enemyPart, child.position, child.rotation, child);
                partObjTemp.transform.localScale = child.localScale;
            }
        }
        
        
        
        //set the spawned enemy data to the EnemyType variable
        SpawnedEnemy.GetComponent<Enemy>().data = EnemyType;

    }
    
}
