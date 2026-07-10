using JetBrains.Annotations;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector] public Transform Player;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float sensitivity = 100f;
    [SerializeField] private float minHeight = 10f;
    [SerializeField] float rotationY = 0f; // can be used to offset camera height
    [SerializeField] private float rotationMin;
    [SerializeField] private float rotationMax;
    float rotationX = 0f;

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        //change values to change vertical clamping
        rotationY = Mathf.Clamp(rotationY, rotationMin, rotationMax);

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 desiredPosition = Player.transform.position - (rotation * Vector3.forward * distance);

        transform.position = desiredPosition;
        transform.LookAt(Player);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20f))
        {
            if(transform.position.y < hit.point.y + minHeight)
            {
                transform.position = new Vector3 (transform.position.x,hit.point.y + minHeight, transform.position.z);
            }
        }

    }
}
