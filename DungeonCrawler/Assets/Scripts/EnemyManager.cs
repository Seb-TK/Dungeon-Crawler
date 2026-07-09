using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject SeekPointTemp;
    [SerializeField] private GameObject[] EnemyList;
    void Start()
    {
        Instantiate(Enemy, new Vector3(5,0,0), Quaternion.identity);
        Instantiate(SeekPointTemp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
