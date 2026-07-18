using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    
    [Header("General")]
    public string Name;
    public string Class;
    public GameObject VisualMesh;
    public GameObject Bullet;

    [Header("Stats")]
    public float FireRate;
    public float BulletSpeed;
    public float TurnSpeed;
    public float Accuracy;
    public float Health;
    public float Defense;
    public string[] SpecialAttributes;


}
