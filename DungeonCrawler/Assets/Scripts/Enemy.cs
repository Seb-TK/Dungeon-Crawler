using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    EnemyMovement movement;
    EnemyShooting shooting;
    float health;
    public GameObject SeekPointTemp;
    void Start()
    {
        //seek point (for visual debugging)
        GameObject seekPointObj = Instantiate(SeekPointTemp);
        seekPointObj.GetComponent<SeekPointTemp>().EnemyPos = gameObject;
        
        //instantiate and parent mesh
        Instantiate(data.Mesh, transform.position, transform.rotation, transform);

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
