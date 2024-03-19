using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LootTable : MonoBehaviour
{

    [Header("Pickup Drops")]
    [SerializeField] private bool dropsPowerUps = false;
    [SerializeField,Tooltip("Likelyhood of anything dropping."), Range(1,100)] public float dropEventChance;
    public List<GameObject> commonPowerUps;
    [SerializeField, Tooltip("Likelyhood of an item from a specific category dropping"), Range(0,100)] public float commonDropChance;
    public List<GameObject> uncommonPowerUps;
    [SerializeField, Tooltip("Likelyhood of an item from a specific category dropping"), Range(0,100)] public float uncommonDropChance;
    public List<GameObject> rarePowerUps;
    [SerializeField, Tooltip("Likelyhood of an item from a specific category dropping"), Range(0,100)] public float rareDropChance;

    public void DropPowerUp() {

        float dropRoll = Random.Range(1,101);
        
        if (dropsPowerUps)
        {
            if (dropEventChance >= dropRoll)
            {
                float tableRoll = Random.Range(1,101);
                GameObject itemToSpawn = null;
                List<GameObject> listToCheck = new List<GameObject>();
    
                if(rareDropChance >= tableRoll) {
                    listToCheck = rarePowerUps;
                } else if (uncommonDropChance >= tableRoll) {
                    listToCheck = uncommonPowerUps;
                } else if (commonDropChance >= tableRoll) {
                    listToCheck = commonPowerUps;
                }
    
                if (listToCheck.Count > 0)
                {
                    itemToSpawn = listToCheck[Random.Range(0,listToCheck.Count)];
                    
                } else return;
                        
                Instantiate(itemToSpawn,transform.position,Quaternion.identity);
            }  
        }   
    }
}
