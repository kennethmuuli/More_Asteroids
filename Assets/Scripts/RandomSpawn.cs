using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// paigutab asteroidid juhuslikult mänguväljakul, kui mäng algab
public class RandomSpawn : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);
    }
}
