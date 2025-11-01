using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damage = 1;              // Скільки шкоди завдає
    public float attackCooldown = 10f;  // Затримка між атаками (10 секунд)
    private float lastAttackTime = -999f;

    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        agent.SetDestination(player.position);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TryAttack(collision.gameObject);
        }
    }

    private void TryAttack(GameObject playerObj)
    {
        // Якщо минуло достатньо часу від останньої атаки
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Player playerHealth = playerObj.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }
}
