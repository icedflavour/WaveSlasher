using UnityEngine;

namespace Combat.Items
{
    public class RangedWeapon : WeaponBase
    {
        protected override void PerformAttack()
        {
            // Default direction: forward
            Vector2 direction = transform.right;

            // For auto-attack controller, direction will be passed separately
            SpawnProjectile(direction);

            if (stats.attackSFX)
                AudioSource.PlayClipAtPoint(stats.attackSFX, attackOrigin.position);
        }

        public void TryAttack(Vector2 direction)
        {
            if (attackCooldown > 0f)
                return;

            SpawnProjectile(direction);
            attackCooldown = 1f / stats.attackSpeed;
        }
    }
}
