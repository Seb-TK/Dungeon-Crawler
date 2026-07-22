using System.Runtime.InteropServices;
using Unity.Properties;
using UnityEngine;

[CreateAssetMenu(fileName = "StateItemTemplate", menuName = "Scriptable Objects/StateItemTemplate")]
public class StateItemTemplate : ScriptableObject
{
    [Header("General")]
    public string Name;
    //ultra shit
    //Common
    //Uncommon
    //Rare
    //Epic
    //Mythical
    //Legendary
    //Unreal
    public string Rarity;

    [Header("Health")]
    public float Health;
    public float Defense;
    public float Regen;

    [Header("Movement")]
    public float Speed;
    public float MaxNitro;
    public float NitroSpeed;
    public float NitroRegen;
    
    [Header("Melee")]
    public float Damage;
    public float MeleeCritChance;
    public float MeleeCritMultiplier;
    

    [Header("Ranged")]
    public float RangedDamage;
    public float RangedCritChance;
    public float RangedCritDamage;
    public float Accuracy;
    public float FireRate;

    [Header("Droppers")]
    public float DropperDamage;
    public float DropperCritChance;
    public float DropperCritDamage;

    [Header("BulletEffects")]
    public float EffectChance;
    // how powerful the effect itself is, 
    // more unique than damage like how many bounces
    public float EffectStrength;
    public float EffectDuration;
    public float EffectDamage;
    public float EffectCritChance;
    public float EffectCritDamage;

    
}
