using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidDestruction : ObjectToDestroy
{
    [SerializeField] private AudioSource destructionSound;

    protected override void Die()
    {
        if (iGotHit) {
            destructionSound.Play();
            base.Die();
        }
    }
}
