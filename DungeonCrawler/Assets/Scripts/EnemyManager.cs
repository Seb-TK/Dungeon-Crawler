using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private EnemyData[] EnemyList;
    //potential spawn locations (temp for testing)
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minZ;
    [SerializeField] float maxZ;
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
        EnemyData EnemyType = EnemyList[EnemyIndex];
        GameObject SpawnedEnemy = Instantiate(Enemy, spawnPosition, Quaternion.identity);
        //set the spawned enemy data to the EnemyType variable
        SpawnedEnemy.GetComponent<Enemy>().data = EnemyType;

    }
    
}
