using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [Header("Player 2 Assets")]
    [SerializeField] private Sprite player2Sprite;
    [SerializeField] private AnimatorController player2AnimatorController;
    private int instanceID;
    private bool isCoopGame;
    
    private void Awake() {
        instanceID = transform.GetInstanceID();
    }
    private void OnEnable() {
        GameManager.OnPublishPlayer += Trigger2PlayerColorSwitch;
        GameManager.AnnounceCoopGame += ToggleCoopGame;
    }

    private void Trigger2PlayerColorSwitch(int instanceIDToCheck, GameObject playerObject){
        if (isCoopGame && instanceID == instanceIDToCheck)
        {
            SwitchColor();
        }
    }

    private void ToggleCoopGame(){
        isCoopGame = true;
    }
    private void SwitchColor(){
        spriteRenderer.sprite = player2Sprite;
        animator.runtimeAnimatorController = player2AnimatorController;
    }
}
