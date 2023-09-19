using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerMovement : MonoBehaviour {
    public float gravityValue = -9.81f;
    public float jumpHeight = 1.0f;

    public float playerSpeed = 0.5f;
    private InputReader _inputReader;
    private Vector2 _moveInput;
    private CharacterController characterController;
    private Vector3 playerVelocity;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        _inputReader = GetComponent<InputReader>();
    }

    private void FixedUpdate() {
        // our update loop polls the "move" action value each frame
        _moveInput = _inputReader._moveVector;
        //Debug.Log($"Move vector value: {_moveInput}");
        Move(_moveInput);

        if (characterController.isGrounded) {
            //Debug.Log("Is grounded!");
        }
        else {
            //Debug.Log("Is NOT grounded!");
            // Press the character down to the floor to avoid jitter "true-false"
            // of the isGrounded property.
            // To do it, add some small gravity (or velocity in your terms).
            playerVelocity.y = gravityValue * 0.1f;
            characterController.Move(playerVelocity);
        }
    }

    public void Jump() {
        if (characterController.isGrounded) {
            Debug.Log("Can jump, is grounded!");
            //playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            playerVelocity.y = jumpHeight + -gravityValue;
        }

        characterController.Move(playerVelocity);
    }

    private void Move(Vector2 movementVector) {
        playerVelocity.x = movementVector.x * playerSpeed;
        playerVelocity.z = movementVector.y * playerSpeed;

        characterController.Move(playerVelocity);
    }
}