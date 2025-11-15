using UnityEngine;
using Core;

namespace Enemy
{
    public enum EnemyState
    {
        Passive,
        Active
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyAIController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float stopDistance = 0.5f;

        private Transform player;
        private Rigidbody2D rb;
        private EnemyState currentState = EnemyState.Passive;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            player = PlayerRegistry.Player;
        }

        private void Update()
        {
            switch (currentState)
            {
                case EnemyState.Passive:
                    rb.linearVelocity = Vector2.zero;
                    break;

                case EnemyState.Active:
                    if (player != null)
                        MoveTowardsPlayer();
                    break;
            }
        }

        private void MoveTowardsPlayer()
        {
            Vector2 direction = (player.position - transform.position);
            float distance = direction.magnitude;

            if (distance > stopDistance)
            {
                rb.linearVelocity = direction.normalized * moveSpeed;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }

        public void SetState(EnemyState newState)
        {
            currentState = newState;
        }

        public EnemyState GetState()
        {
            return currentState;
        }
    }
}
