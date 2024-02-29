using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    private Vector2 _movementVector;
    private bool _isBoosting, _isFiring, _isAskedToJoin;

    private void Update() {
        // print(_isBoosting);
       
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
    public void OnJoinUpdated(InputAction.CallbackContext context) {
        _isAskedToJoin = context.ReadValue<float>() > 0.5f;
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
