using UnityEngine;
using Combat.Items;
using Core;

namespace Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private float targetDetectionRadius = 8f;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private bool autoAttack = true;

        private WeaponBase currentWeapon;
        private Transform currentTarget;
        private float retargetDelay = 0.5f;
        private float retargetTimer;

        private void Awake()
        {
            // Find weapon in children (MeleeWeapon or RangedWeapon)
            currentWeapon = GetComponentInChildren<WeaponBase>();

            if (currentWeapon == null)
                Debug.LogError("[PlayerAttackController] No weapon found in children!");
        }

        private void Update()
        {
            if (!autoAttack || currentWeapon == null)
                return;

            retargetTimer -= Time.deltaTime;

            // Periodically search for nearest target
            if (currentTarget == null || retargetTimer <= 0f)
            {
                currentTarget = FindNearestTarget();
                retargetTimer = retargetDelay;
            }

            // Attack if a target exists
            if (currentTarget != null)
                HandleAttack();
        }

        /// <summary>
        /// Finds the nearest enemy within detection radius.
        /// </summary>
        private Transform FindNearestTarget()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                transform.position,
                targetDetectionRadius,
                enemyLayer
            );

            float closestDistance = Mathf.Infinity;
            Transform nearest = null;

            foreach (var hit in hits)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearest = hit.transform;
                }
            }

            return nearest;
        }

        /// <summary>
        /// Handles attack logic depending on weapon type.
        /// </summary>
        private void HandleAttack()
        {
            Vector2 direction = (currentTarget.position - transform.position).normalized;

            if (currentWeapon is RangedWeapon ranged)
                ranged.TryAttack(direction);
            else
                currentWeapon.TryAttack();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, targetDetectionRadius);
        }
#endif
    }
}
