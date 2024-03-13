using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputReader : MonoBehaviour
{
    private Vector2 _movementVector;
    private bool _isBoosting, _isFiring, _isAskedToJoin;
    private PlayerInput playerInput;

    private void OnEnable() {
        GameManager.OnUpdateGameState += ToggleInputMap;
    }
    private void OnDisable() {
        GameManager.OnUpdateGameState -= ToggleInputMap;
    }
    private void Start(){
        GameManager.instance.PublishPlayerID(transform.GetInstanceID(), gameObject);
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMovementUpdated(InputAction.CallbackContext context) {
        _movementVector = context.ReadValue<Vector2>();
    }
    public void OnBoostUpdated(InputAction.CallbackContext context) {
        _isBoosting = context.ReadValue<float>() > 0.5f;
    }
    public void OnShootUpdated(InputAction.CallbackContext context) {
        _isFiring = context.ReadValue<float>() > 0.5f;
    }
    public void OnPauseUpdated(InputAction.CallbackContext context) {
        if (context.started)
        {
            GameManager.instance.UpdateGameState(GameState.Pause);
        }
    }
    public void OnConfirmUpdated(InputAction.CallbackContext context) {
        // if(context.started) {
        //     Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        //     button.onClick.Invoke();
        // }
    }

    private void ToggleInputMap(GameState gameState) {
        // if (gameState == GameState.Play)
        // {
        //     playerInput.SwitchCurrentActionMap("Player");
        //     print("inputmap = " + playerInput.currentActionMap.name);
        // } else if (gameState == GameState.Pause) {
        //     playerInput.SwitchCurrentActionMap("UI");
        //     print("inputmap = " + playerInput.currentActionMap.name);
        // }
    }

    public Vector2 MovementVector{
        get {return _movementVector;}
    }
    public bool IsBoosting{
        get {return _isBoosting;}
    }
    public bool IsFiring{
        get {return _isFiring;}
    }
    public bool IsAskedToJoin{
        get {return _isAskedToJoin;}
    }
}
