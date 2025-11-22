using UnityEngine;
using Core;

namespace Enemy
{
    [RequireComponent(typeof(HealthController))]
    public class EnemyHealthController : MonoBehaviour, IDamageable
    {
        private HealthController healthController;

        public float CurrentHealth => healthController.CurrentHealth;
        public float MaxHealth => healthController.MaxHealth;
        public bool IsAlive => healthController.IsAlive;

        public event System.Action<float, float> OnHealthChanged
        {
            add => healthController.OnHealthChanged += value;
            remove => healthController.OnHealthChanged -= value;
        }

        public event System.Action OnDeath
        {
            add => healthController.OnDeath += value;
            remove => healthController.OnDeath -= value;
        }

        public event System.Action<float> OnHeal
        {
            add => healthController.OnHeal += value;
            remove => healthController.OnHeal -= value;
        }

        private void Awake()
        {
            healthController = GetComponent<HealthController>();
        }

        public void TakeDamage(float amount)
        {
            healthController.TakeDamage(amount);
        }

        public void Heal(float amount)
        {
            healthController.Heal(amount);
        }
    }
}
