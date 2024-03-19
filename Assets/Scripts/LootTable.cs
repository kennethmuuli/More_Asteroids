using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootTable")]
public class LootTable : ScriptableObject
{
    [SerializeField,Tooltip("Likelyhood of anything dropping.")] public float dropEventChance;
    public List<GameObject> commonPowerUps;
    [SerializeField, Tooltip("Likelyhood of an item from a specific category dropping"), Range(0,100)] public float commonDropChance;
    public List<GameObject> uncommonPowerUps;
    [SerializeField, Tooltip("Likelyhood of an item from a specific category dropping"), Range(0,100)] public float uncommonDropChance;
    public List<GameObject> rarePowerUps;
    [SerializeField, Tooltip("Likelyhood of an item from a specific category dropping"), Range(0,100)] public float rareDropChance;
}
