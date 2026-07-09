using Unity.VisualScripting;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float move;
    private float turn;
    [SerializeField] TrailRenderer skidMarks;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float driftMultiplier;
    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("hi1");
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        Debug.Log("Camera found: " + (cam != null));

        CameraFollow cf = cam.GetComponent<CameraFollow>();
        Debug.Log("CameraFollow found: " + (cf != null));

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().Player = transform;
        Debug.Log("hi2");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move = Input.GetAxis("Vertical");
        turn = Input.GetAxis("Horizontal");
    
        rb.AddForce(transform.forward * move * moveSpeed);
        if (Input.GetKey(KeyCode.Space))
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
