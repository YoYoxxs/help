using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Settings")]
    private PlayerInputActions playerInputActions;

    [Header("Components")]
    private PlayerMover playerMover;

    // Input values
    private Vector2 movementInput;

    private void Awake()
    {
        // Get the PlayerMover component
        playerMover = GetComponent<PlayerMover>();

        if (playerMover == null)
        {
            Debug.LogError("PlayerInputHandler: No PlayerMover component found on " + gameObject.name);
        }

        // Initialize input actions
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        // Enable the input actions
        playerInputActions.Enable();

        // Subscribe to the movement input event
        playerInputActions.Player.Movement.performed += OnMovementInput;
        playerInputActions.Player.Movement.canceled += OnMovementInput;
    }

    private void OnDisable()
    {
        // Unsubscribe from input events
        playerInputActions.Player.Movement.performed -= OnMovementInput;
        playerInputActions.Player.Movement.canceled -= OnMovementInput;

        // Disable the input actions
        playerInputActions.Disable();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        // Read the movement input as Vector2
        movementInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        // Call the Move method on PlayerMover with the current input
        if (playerMover != null)
        {
            playerMover.Move(movementInput);
        }
    }

    // Public method to get current movement input (useful for other scripts)
    public Vector2 GetMovementInput()
    {
        return movementInput;
    }
}