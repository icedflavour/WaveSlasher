using UnityEngine;
using Core;
using Combat.Data;

namespace Combat.Items
{
    [RequireComponent(typeof(Collider2D))]
    public class ProjectileBase : MonoBehaviour
    {
        protected CombatStats stats;
        protected WeaponBase ownerWeapon;
        protected Vector2 direction;
        protected Rigidbody2D rb;
        protected float lifeTime = 5f;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public virtual void Initialize(CombatStats stats, Vector2 direction, WeaponBase owner)
        {
            this.stats = stats;
            this.direction = direction.normalized;
            this.ownerWeapon = owner;

            if (rb != null)
                rb.linearVelocity = this.direction * stats.projectileSpeed;

            Destroy(gameObject, lifeTime);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage(stats.damage);
                OnHitTarget(target);
            }

            // disable further collisions
            var col = GetComponent<Collider2D>();
            if (col) col.enabled = false;

            if (rb) rb.linearVelocity = Vector2.zero;

            Destroy(gameObject, 0.05f);
        }

        protected virtual void OnHitTarget(IDamageable target)
        {
            if (stats.attackVFX != null)
                Instantiate(stats.attackVFX, transform.position, Quaternion.identity);
        }
    }
}
