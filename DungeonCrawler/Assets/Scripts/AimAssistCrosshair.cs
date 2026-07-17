using UnityEngine;

public class AimAssistCrosshair : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    CameraFollow cameraScript;
    public GameObject targetedEnemy;
    [SerializeField] public float AimAssistRadius;
    [SerializeField] public float AimAssistRange;
    Transform Player;

    // Update is called once per frame
    void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        //finding targeted enemies
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

        //locking on
        if(targetedEnemy == null){
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            transform.position = Camera.main.WorldToScreenPoint(targetedEnemy.transform.position);
        }
    }
}
