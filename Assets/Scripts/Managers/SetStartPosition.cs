using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartPosition : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
