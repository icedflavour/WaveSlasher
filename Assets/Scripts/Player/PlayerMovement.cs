using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MovementController
{
    private PlayerInput playerInput;
    private InputAction moveAction;

    protected override void Awake()
    {
        base.Awake();

        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
            moveAction = playerInput.actions["Move"];
        else
            Debug.LogError("PlayerInput component not found on " + gameObject.name);
    }

    private void OnEnable()
    {
        if (playerInput != null)
            playerInput.actions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        if (playerInput != null)
            playerInput.actions.FindActionMap("Player").Disable();
    }

    private void Update()
    {
        if (moveAction != null)
            moveDirection = moveAction.ReadValue<Vector2>();
    }
}
