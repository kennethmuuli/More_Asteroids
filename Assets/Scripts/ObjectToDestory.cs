using System;
using UnityEngine;

public class ObjectToDestory : MonoBehaviour
{
    [SerializeField] private int myScoreValue;
    public static event Action<int> objectDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        
        /* 
            TODO: The code below should be placed into some sort of general Die(), which all destructible objects need to have. Consider creating an interface for that purpose OR perhaps an abstract class instead. Currently in start method simply for testing purposes.
        */

        // syntax> ? Checking if anyone is subscribed to the objectDestoryed event to avoid a null reference error, IF yes then invoke the event (i.e. event happened) and pass along myScoreValue
        objectDestroyed?.Invoke(myScoreValue);
        Destroy(gameObject);
    }

}
