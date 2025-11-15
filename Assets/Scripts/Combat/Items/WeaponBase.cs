using UnityEngine;
using Combat.Data;

namespace Combat.Items
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [Header("Combat Settings")]
        [SerializeField] protected CombatStats stats;
        [SerializeField] protected Transform attackOrigin;

        protected float attackCooldown;
        protected bool canAttack = true;

        public CombatStats Stats => stats;

        protected virtual void Update()
        {
            if (attackCooldown > 0f)
                attackCooldown -= Time.deltaTime;
        }

        /// <summary>
        /// External attack trigger without direction (melee or default).
        /// </summary>
        public virtual void TryAttack()
        {
            if (!CanFire()) return;

            PerformAttack(transform.right);
            OnAttackFired();
        }

        /// <summary>
        /// External attack trigger with direction (ranged auto-target).
        /// </summary>
        public virtual void TryAttack(Vector2 direction)
        {
            if (!CanFire()) return;

            PerformAttack(direction.normalized);
            OnAttackFired();
        }

        /// <summary>
        /// Checks if weapon can fire.
        /// </summary>
        protected bool CanFire()
        {
            return canAttack && attackCooldown <= 0f && stats != null;
        }

        /// <summary>
        /// Local use by subclasses (melee / ranged).
        /// </summary>
        protected abstract void PerformAttack(Vector2 direction);

        /// <summary>
        /// Handles sound, vfx and cooldown.
        /// </summary>
        protected void OnAttackFired()
        {
            attackCooldown = 1f / stats.attackSpeed;

            if (stats.attackSFX)
                AudioSource.PlayClipAtPoint(stats.attackSFX, attackOrigin.position);

            if (stats.attackVFX)
                Instantiate(stats.attackVFX, attackOrigin.position, Quaternion.identity);
        }

        /// <summary>
        /// Utility to spawn projectile.
        /// </summary>
        protected GameObject SpawnProjectile(Vector2 direction)
        {
            if (stats.projectilePrefab == null)
                return null;

            GameObject obj = Instantiate(
                stats.projectilePrefab,
                attackOrigin.position,
                Quaternion.identity
            );

            if (obj.TryGetComponent<ProjectileBase>(out var proj))
            {
                proj.Initialize(stats, direction, this);
            }

            return obj;
        }

        public virtual void StopAttack() => canAttack = false;
        public virtual void ResumeAttack() => canAttack = true;
    }
}
