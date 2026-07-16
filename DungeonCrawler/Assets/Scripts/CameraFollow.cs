using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Animations;

public class CameraFollow : MonoBehaviour
{
    [HideInInspector] public Transform Player;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float sensitivity = 100f;
    [SerializeField] private float minHeight = 10f;
    [SerializeField] float rotationY = 0f; // can be used to offset camera height
    [SerializeField] private float rotationMin;
    [SerializeField] private float rotationMax;
    [SerializeField] private float xAxisTilt;
    float rotationX = 0f;
    public GameObject targetedEnemy;
    public float AimAssistRadius;
    public float AimAssistRange;
    

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
        transform.Rotate(xAxisTilt, 0f,0f);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 20f))
        {
            if(transform.position.y < hit.point.y + minHeight)
            {
                transform.position = new Vector3 (transform.position.x,hit.point.y + minHeight, transform.position.z);
            }
        }

        //Aim assist alg for locking onto enemies
        Vector2 screenCenter = new Vector2 (Screen.width / 2f, Screen.height / 2f);
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        targetedEnemy = null;
        float targetedEnemyDistance = AimAssistRadius;
        foreach (GameObject Enemy in Enemies)
        {
            float tempDistance = Vector2.Distance(Camera.main.WorldToScreenPoint(Enemy.transform.position), screenCenter);
            if (tempDistance < targetedEnemyDistance & Vector3.Distance(Player.transform.position, Enemy.transform.position) < AimAssistRange)
            {
                targetedEnemyDistance = tempDistance;
                targetedEnemy = Enemy;
            }
        }


    }
}
