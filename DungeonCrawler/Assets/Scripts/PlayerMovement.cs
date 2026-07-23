using Unity.VisualScripting;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [HideInInspector] public bool nitroFatigue;
    [SerializeField] TrailRenderer skidMarks;
    [SerializeField] TrailRenderer nitroTrail;

    [SerializeField] public float moveSpeed;
    [SerializeField] public float turnSpeed;
    [SerializeField] public float driftMultiplier;
    [SerializeField] public float nitroMultiplier;
    [SerializeField] public float maxNitro;
    [SerializeField] public float nitroUseSpeed;
    [SerializeField] public float nitroRegenSpeed;
    //using nitro can make you heavier so you push harder 
    // (also do more melee damage?)
    [SerializeField] public float nitroMassMultiplier;
    [SerializeField] public float playerMass;
    [SerializeField] public float nitroFatigueThreshold;
    [SerializeField] public float nitroFatigueMultiplier;
    
    public float nitroFuel;
    float move;
    float turn;
    Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        CameraFollow cf = cam.GetComponent<CameraFollow>();
        
        GameObject.FindGameObjectWithTag("MainCamera")
                        .GetComponent<CameraFollow>()
                        .Player = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move = Input.GetAxis("Vertical");
        turn = Input.GetAxis("Horizontal");
        nitroTrail.emitting = false;
        //if youre pressing down then 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //always decrease nitro
            nitroFuel -= nitroUseSpeed;
            // nitro doesn't work if you're fatigued even if you're pressing
            if(nitroFatigue)
            {
                rb.AddForce(transform.forward * move * moveSpeed);
                rb.mass = playerMass;
            }
            //if youre not fatigued then increase speed and mass
            else
            {
                rb.AddForce(transform.forward * move * nitroMultiplier * moveSpeed);
                rb.mass = playerMass * nitroMassMultiplier;
                nitroTrail.emitting = true;
            }
        // if youre not pressing anything
        //if youre fatigued increase slowly
        } else if (nitroFatigue)
            {
                rb.AddForce(transform.forward * move * moveSpeed);
                rb.mass = playerMass;
                nitroFuel += nitroRegenSpeed * nitroFatigueMultiplier;
            }
        else //if not increase normally
            {
                rb.AddForce(transform.forward * move * moveSpeed);
                rb.mass = playerMass;
                nitroFuel += nitroRegenSpeed;
            }
        
        //
        if(nitroFuel <= 0)
        {
            nitroFuel = 0;
            nitroFatigue = true;
        } else if(nitroFuel > maxNitro)
        {
            nitroFuel = maxNitro;
        }
        //nitro fatiuge, you get slower nitro regen and can't nitro when low
        if(nitroFuel > nitroFatigueThreshold)
        {
            nitroFatigue = false;
        }
        
        
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Rotate(0, turn * turnSpeed * driftMultiplier * (rb.linearVelocity.magnitude / 10), 0);
            skidMarks.emitting = true;
        }
        else
        {
            transform.Rotate(0, turn * turnSpeed * (rb.linearVelocity.magnitude / 10), 0);
            skidMarks.emitting = false;
        }
    }

}
