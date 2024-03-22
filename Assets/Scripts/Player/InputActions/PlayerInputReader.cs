using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputReader : MonoBehaviour
{
    private Vector2 _movementVector;
    private bool _isBoosting, _isFiring, _isAskedToJoin;
    private PlayerInput playerInput;
    private string savedControlSchemeName;
    private InputDevice inputDevice;
    private bool notInitialEnable;
    private bool readInput = true;

    private void OnEnable() {
        GameManager.OnUpdateGameState += ToggleInputMap;
        
        // Reset initial controlscheme and device
        if (notInitialEnable)
        {
            playerInput.SwitchCurrentControlScheme(savedControlSchemeName, inputDevice);
        }
    }
    private void OnDisable() {
        GameManager.OnUpdateGameState -= ToggleInputMap;
    }
    private void Start(){
        GameManager.instance.PublishPlayerID(transform.GetInstanceID(), gameObject);
        playerInput = GetComponent<PlayerInput>();

        // save initial controlscheme and device to reattach when enabled, otherwise it conflicts with the keyboardSplitter and reset controller to keyboard
        if (!notInitialEnable)
        {
            savedControlSchemeName = playerInput.currentControlScheme;
            inputDevice = playerInput.devices[0];
            notInitialEnable = true;
        }

    }

    public void OnMovementUpdated(InputAction.CallbackContext context) {
        if (readInput)
        {
            _movementVector = context.ReadValue<Vector2>();
        }
    }
    public void OnBoostUpdated(InputAction.CallbackContext context) {
        if (readInput)
        {
            _isBoosting = context.ReadValue<float>() > 0.5f;
        }
    }
    public void OnShootUpdated(InputAction.CallbackContext context) {
        if (readInput)
        {
            _isFiring = context.ReadValue<float>() > 0.5f;
        }
    }
    public void OnPauseUpdated(InputAction.CallbackContext context) {
        if (context.started)
        {
            GameManager.instance.UpdateGameState(GameState.Pause);
        }
    }
    public void OnConfirmUpdated(InputAction.CallbackContext context) {

    }

    private void ToggleInputMap(GameState gameState) {
        if(gameState == GameState.Play) {
            readInput = true;
        } else if (gameState == GameState.Pause) {

            readInput = false;
        }
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
