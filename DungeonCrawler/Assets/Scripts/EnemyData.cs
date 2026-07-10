using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("General")]
    public GameObject Mesh;
    public float Health;

    [Header("Movement")]
    public float MoveSpeed;
    public float TurnSpeed;
    public float DriftMultiplier;
    public string AiType;

    [Header("Shooting")]
    public GameObject Bullet;
    public float FireRate;

}
