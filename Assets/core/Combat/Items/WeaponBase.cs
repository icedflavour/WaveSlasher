using UnityEngine;
using Combat.Data;
using Core;

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
        /// Attempts to perform an attack if cooldown allows.
        /// </summary>
        public virtual void TryAttack()
        {
            if (!canAttack || attackCooldown > 0f || stats == null)
                return;

            PerformAttack();
            attackCooldown = 1f / stats.attackSpeed;
        }

        /// <summary>
        /// Core attack logic, implemented by derived classes.
        /// </summary>
        protected abstract void PerformAttack();

        /// <summary>
        /// Spawns a projectile using stats.projectilePrefab.
        /// </summary>
        protected virtual void SpawnProjectile(Vector2 direction)
        {
            if (stats.projectilePrefab == null)
                return;

            GameObject projectile = Instantiate(
                stats.projectilePrefab,
                attackOrigin.position,
                Quaternion.identity
            );

            if (projectile.TryGetComponent<ProjectileBase>(out var proj))
            {
                proj.Initialize(stats, direction, this);
            }
        }

        /// <summary>
        /// Stops attacking (used when weapon disabled or player dead).
        /// </summary>
        public virtual void StopAttack()
        {
            canAttack = false;
        }

        /// <summary>
        /// Resumes attacking after stop.
        /// </summary>
        public virtual void ResumeAttack()
        {
            canAttack = true;
        }
    }
}
