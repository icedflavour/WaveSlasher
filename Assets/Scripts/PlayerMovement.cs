using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int Score;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    public float moveSpeed;
    private GameObject Player;
    private Vector2 PlayerPosition; 

    private void FindPlayer()
    {
        if (Player != null)
        {
            PlayerPosition = Player.transform.position;
        }
    }

    private void BuildPath()
    {

    }

    public enum CharacterType
    {
        NPC,
        Player
    }

    public CharacterType selectedCharacter;
    
    //if (selectedCharacter == CharacterType.Player)
    //{
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collectable")
        {
            Score += 1;
            Destroy(other);
        }
    }
    //}

    //if (selectedCharacter == CharacterType.Enemy)
    //{

    //}


}   
