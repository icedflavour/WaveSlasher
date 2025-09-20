using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;

    [Header("Налаштування руху")]
    [SerializeField] private float moveSpeed = 5f; // швидкість можна змінювати в інспекторі

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        // рух у фіксованій фізичній системі
        rb.linearVelocity = moveInput * moveSpeed;
    }
}
