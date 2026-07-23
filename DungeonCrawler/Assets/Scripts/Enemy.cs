using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    EnemyMovement movement;
    EnemyShooting shooting;
    public float health;
    public GameObject SeekPointTemp;
    GameObject visualMesh;
    void Start()
    {
        Debug.Log("Hi");
        Debug.Log("Shooting.Bullet: " + shooting.Bullet);
        Debug.Log("data.Bullet: " + data.Bullet);
        //seek point (for visual debugging)
        GameObject seekPointObj = Instantiate(SeekPointTemp);
        seekPointObj.GetComponent<SeekPointTemp>().EnemyPos = gameObject;
        
        //instantiate and parent mesh
        visualMesh = Instantiate(data.VisualMesh, transform.position, transform.rotation, transform);

        //use scriptable object's data to set variables
        health = data.Health;


        movement = GetComponent<EnemyMovement>();
        shooting = GetComponent<EnemyShooting>();

        // set variables in other scripts
        movement.moveSpeed = data.MoveSpeed;
        movement.turnSpeed = data.TurnSpeed;
        movement.driftMultiplier = data.DriftMultiplier;
        movement.AiType = data.AiType;


        shooting.Bullet = data.Bullet;
        shooting.fireRate = data.FireRate;
        shooting.bulletSpeed = data.BulletSpeed;

    }
}
