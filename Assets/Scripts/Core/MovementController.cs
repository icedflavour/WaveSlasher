using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float moveSpeed = 5f;

    protected Rigidbody2D rb;
    protected NavMeshAgent agent;
    protected Vector2 moveDirection;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = moveSpeed;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (agent == null)
            rb.linearVelocity = moveDirection * moveSpeed;
        else
            agent.speed = moveSpeed;
    }

    public virtual void Stop()
    {
        if (agent != null)
            agent.ResetPath();
        else
            rb.linearVelocity = Vector2.zero;
    }
}
