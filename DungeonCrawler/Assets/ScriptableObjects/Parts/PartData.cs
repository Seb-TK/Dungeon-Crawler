using UnityEngine;
//shoulda been part fart
[CreateAssetMenu(fileName = "PartData", menuName = "Scriptable Objects/PartData")]
public class PartData : ScriptableObject
{
    public string Name;
    public string Quality;
    public float Health;
    public float Defense;
    
}
