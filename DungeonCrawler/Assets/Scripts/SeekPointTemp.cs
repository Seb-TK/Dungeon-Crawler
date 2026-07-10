using UnityEngine;

public class SeekPointTemp : MonoBehaviour
{
    public GameObject EnemyPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    // Update is called once per frame
    void Update()
    {
        transform.position = EnemyPos.GetComponent<EnemyMovement>().SeekPoint;
    }
}
