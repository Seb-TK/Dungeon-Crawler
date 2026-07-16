using UnityEngine;

public class AimAssistCrosshair : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject targetedEnemy;
    CameraFollow cameraScript;

    // Update is called once per frame
    void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }
    void Update()
    {
        targetedEnemy = cameraScript.targetedEnemy;
        if(targetedEnemy == null){
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            transform.position = Camera.main.WorldToScreenPoint(targetedEnemy.transform.position);
        }
    }
}
