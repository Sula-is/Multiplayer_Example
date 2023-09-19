using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour {
    public InputActionAsset actions;

    public UnityEvent _OnJump = new();
    public UnityEvent _OnMove = new();

    [HideInInspector] public Vector2 _moveVector;

    private InputAction moveAction;

    private void Awake() {
        // for the "jump" action, we add a callback method for when it is performed
        actions.FindActionMap("Player").FindAction("Jump").performed += OnJump;

        // find the "move" action, and keep the reference to it, for use in Update
        moveAction = actions.FindActionMap("Player").FindAction("Move");
    }

    private void FixedUpdate() {
        // our update loop polls the "move" action value each frame
        _moveVector = moveAction.ReadValue<Vector2>();
        if (_moveVector != Vector2.zero) {
            _OnMove?.Invoke();
        }
    }

    private void OnEnable() {
        actions.FindActionMap("Player").Enable();
    }

    private void OnDisable() {
        actions.FindActionMap("Player").Disable();
    }

    private void OnJump(InputAction.CallbackContext context) {
        // this is the "jump" action callback method
        Debug.Log("Jump!");
        _OnJump?.Invoke();
    }
}