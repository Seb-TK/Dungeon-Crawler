using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NUnit.Framework;
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

    private float seekPointTimer;
    Rigidbody rb;
    public Transform PlayerTransform;
    
    public Vector3 SeekPoint; // placed here for debugging
    
    private float reverseTimer;
    private int reverse;
    private bool isColliding;

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
        seekPointTimer = Time.time;
    }
    void Update()
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

        if (turn > 0.4f | turn < -0.4f)
            {
                drifting = true;
            } else if (turn < 0.2f | turn > -0.2f)
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
        //retry if object is in the way
        RaycastHit hit2;
        Vector3 direction = (SeekPoint - transform.position).normalized;
        if (Physics.SphereCast(transform.position, 0.5f, direction, out hit2, 10f))
            {
                PickNewSeekPoint();
            }
            
            // retry if seek point is too far 
        if (Vector3.Distance(SeekPoint,PlayerTransform.position) > MaxDistance)
            {
                // if player is way too close to player then accept points that are too far
                if (Vector3.Distance(PlayerTransform.position, transform.position) > MinDistance / 2){
                    PickNewSeekPoint();
                }
            }

        // if player is way too close to player then accept points that are too far
        if (Vector3.Distance(PlayerTransform.position, SeekPoint) < MinDistance){
            PickNewSeekPoint();
        }
            
            
            //retry if seek point is too close
        // if (Vector3.Distance(SeekPoint,PlayerTransform.position) < MinDistance)
        //     {
        //         PickNewSeekPoint();
        //     }
            
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

        } else if(AiType == "Melee")
        {
            SeekPoint = player.transform.position;
        }

    }

    void FixedUpdate()
    {
        // decide what to do to get closer to seek point
        // or check for close objects and move away from them
        
        //applying decision forces to actually move
        if (Time.time - seekPointTimer > 0.1f){
            rb.AddForce(transform.forward * Move * moveSpeed * reverse);
            if (drifting)
            {
                transform.Rotate(0, turn * turnSpeed * driftMultiplier * (rb.linearVelocity.magnitude / 10), 0);
                skidMarks.emitting = true;
            } else
            {
                transform.Rotate(0, turn * turnSpeed * (rb.linearVelocity.magnitude / 10), 0);
                skidMarks.emitting = false;
            }
        }


        //after a moment it starts to reverse until timer reaches zero
        if (reverseTimer > 20)
        {
            reverseTimer --;
        } else if(reverseTimer > 0)
        {
            reverse = -1;
            reverseTimer --;
        }
        else
        {
            reverse = 1;
        }
    }

    //whenever it hits something activate a timer
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            reverseTimer = 40;
        }
        if (collision.gameObject.CompareTag("Obs"))
        {
            reverseTimer = 40;
        }
    }
    

}
