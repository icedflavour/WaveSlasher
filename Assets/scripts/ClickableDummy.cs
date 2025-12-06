using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ClickableDummy : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHP = 100f;
    private float currentHP;

    [Header("UI")]
    public Slider healthSlider;

    private Camera mainCamera;
    private PlayerInput playerInput;
    private InputAction clickAction;

    private void Awake()
    {
        mainCamera = Camera.main;

        currentHP = maxHP;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHP;
            healthSlider.value = maxHP;
        }

        playerInput = FindAnyObjectByType<PlayerInput>();

        if (playerInput != null)
            clickAction = playerInput.actions["Click"]; 
        else
            Debug.LogError("No PlayerInput found in scene!");
    }

    private void OnEnable()
    {
        if (clickAction != null)
            clickAction.performed += OnClick;
    }

    private void OnDisable()
    {
        if (clickAction != null)
            clickAction.performed -= OnClick;
    }

    private void OnClick(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            TakeDamage(10);
        }
    }

    private void TakeDamage(float amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        if (healthSlider != null)
            healthSlider.value = currentHP;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
