using UnityEngine;
using Core;

namespace Combat.Items
{
    public class MeleeWeapon : WeaponBase
    {
        [Header("Melee Settings")]
        [SerializeField] private float hitRadius = 1.5f;
        [SerializeField] private LayerMask enemyLayer;

        protected override void PerformAttack(Vector2 direction)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                attackOrigin.position,
                hitRadius,
                enemyLayer
            );

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<IDamageable>(out var target))
                {
                    target.TakeDamage(stats.damage);
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (attackOrigin == null) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackOrigin.position, hitRadius);
        }
#endif
    }
}
