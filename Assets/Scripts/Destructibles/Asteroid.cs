using UnityEngine;

public class Asteroid : BaseDestructibleObject
{

    // Called on the frame when a script is enabled
    private void Start()
    {
        RandomizeSize();        

        MoveAndSpin(transform.up);
    }

    private void Update() {
        if (iGotHit)
        {
            Die();
        }

        OffScreenBehaviour();
    }
    

}
