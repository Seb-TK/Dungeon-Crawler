using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    
    public float moveSpeed;
    public float turnSpeed;
    public float driftMultiplier;
    public string AiType;
    
    
    [SerializeField] TrailRenderer skidMarks;
    [SerializeField] private int MaxDistance;
    [SerializeField] private int MinDistance;
    [SerializeField] private float SeekRange;
    private float turn;
    private bool drifting;
    private int Move = 1;
    private Collider[] hits;
    private bool collideCheck;

    Rigidbody rb;
    public Transform PlayerTransform;
    
    public Vector3 SeekPoint; // placed here for debugging
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PickNewSeekPoint(); 
    }

    // Update is called once per frame
    void PickNewSeekPoint()
    {
        Vector2 SeekPoint2D = Random.insideUnitCircle * SeekRange;
        SeekPoint = transform.position + new Vector3(SeekPoint2D.x, 0, SeekPoint2D.y);
        drifting = false;
    }
    void FixedUpdate()
    {   
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerTransform = player.transform;
        }
        
        //find seek point and begin to turn towards it
        //this is here so it doesn't spam tap drift while turning
        Vector3 directionToSeekPoint = (SeekPoint - transform.position).normalized;
        turn = Vector3.Dot(transform.right, directionToSeekPoint);

        if (turn > 0.3f | turn < -0.3f)
        {
            drifting = true;
        }
        else
        {
            drifting = false;
        }

        //ai alg for setting seek point to the appropriate location
            if (AiType == "Ranged"){
            //pick a random seek point around you in area
            //If the enemy is at the right distance it stops moving
            if (Vector3.Distance(PlayerTransform.position,transform.position) > MaxDistance | Vector3.Distance(PlayerTransform.position,transform.position) < MinDistance)
            {
                Move = 1;
            }
            else
            {
                Move = 0;
            }
            
            // retry if seek point is too far 
            if (Vector3.Distance(SeekPoint,PlayerTransform.position) > MaxDistance)
            {
                // if player is way too close to player then accept points that are too far
                if (Vector3.Distance(PlayerTransform.position, transform.position) > MinDistance / 2){
                    PickNewSeekPoint();
                }
            }
            
            
            //retry if seek point is too close
            if (Vector3.Distance(SeekPoint,PlayerTransform.position) < MinDistance)
            {
                PickNewSeekPoint();
            }
            
            //checks if seek point isn't too close to obstacles
            hits = Physics.OverlapSphere(SeekPoint, 5);
            collideCheck = false;
            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Obs"))
                {
                    collideCheck = true;
                    break;
                }
            }
            if (collideCheck)
            {
                PickNewSeekPoint();
            }
            //check if seek point is too far from center
            if (Vector3.Distance(SeekPoint,Vector3.zero) > 40)
            {
                PickNewSeekPoint();
            }

            // if seek point is way too close to player
            
            }
        else if(AiType == "Melee")
        {
            SeekPoint = player.transform.position;
        }

        // decide what to do to get closer to seek point
        // or check for close objects and move away from them
        
        //applying decision forces to actually move
        rb.AddForce(transform.forward * Move * moveSpeed);
        if (drifting)
        {
            transform.Rotate(0, turn * turnSpeed * driftMultiplier, 0);
            skidMarks.emitting = true;
        }
        else
        {
            transform.Rotate(0, turn * turnSpeed, 0);
            skidMarks.emitting = false;
        }
    }
}
