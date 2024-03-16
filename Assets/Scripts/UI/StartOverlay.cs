using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOverlay : MonoBehaviour
{
    [SerializeField] private GameObject startOverlay;
    [SerializeField] private GameObject fakeShip;
    
    private void OnEnable() {
        GameManager.OnPublishPlayer += DisableKeyBindOverlay;
    }
    private void OnDisable() {
        GameManager.OnPublishPlayer -= DisableKeyBindOverlay;
    }

    private void DisableKeyBindOverlay(int playerInstanceID, GameObject gameObject) {
        startOverlay.SetActive(false);
        Destroy(fakeShip);
        return;
    }

}
