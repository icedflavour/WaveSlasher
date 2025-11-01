using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // ✅ для перезапуску рівня
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;      // Швидкість руху гравця

    [Header("Camera Follow Settings")]
    public Transform cameraTransform;    // Сюди підтягни головну камеру
    public float cameraFollowSpeed;      // Швидкість руху камери за гравцем
    public Vector3 cameraOffset = new Vector3(0, 0, -10); // Відступ камери

    [Header("Health Settings")]
    public int maxHealth = 5;            // Максимальна кількість ХП
    public int currentHealth;            // Поточне ХП

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Якщо камера не призначена вручну — знайдемо її автоматично
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        // Ініціалізуємо ХП
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            playerInput.actions["Move"].performed += OnMove;
            playerInput.actions["Move"].canceled += OnMove;
        }
    }

    private void OnDisable()
    {
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            playerInput.actions["Move"].performed -= OnMove;
            playerInput.actions["Move"].canceled -= OnMove;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        // 🔹 Камера плавно рухається за гравцем
        if (cameraTransform != null)
        {
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, 0f) + cameraOffset;
            cameraTransform.position = Vector3.Lerp(
                cameraTransform.position,
                targetPos,
                cameraFollowSpeed * Time.fixedDeltaTime
            );
        }
    }

    // ============================================================
    // СИСТЕМА ХП
    // ============================================================

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
