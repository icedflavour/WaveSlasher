using UnityEngine;
using System;

namespace Core
{
    [DisallowMultipleComponent]
    public class HealthController : MonoBehaviour, IDamageable
    {
        [Header("Health Settings")]
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float regenRate = 0f; // HP per second

        public float MaxHealth => maxHealth;
        public float CurrentHealth { get; private set; }
        public bool IsAlive => CurrentHealth > 0f;

        public event Action<float, float> OnHealthChanged; // current, max
        public event Action OnDeath;
        public event Action<float> OnHeal;

        private float regenTimer;

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        private void Update()
        {
            HandleRegeneration();
        }

        private void HandleRegeneration()
        {
            if (regenRate <= 0f || !IsAlive)
                return;

            regenTimer += Time.deltaTime;
            if (regenTimer >= 1f)
            {
                Heal(regenRate);
                regenTimer = 0f;
            }
        }

        public void TakeDamage(float amount)
        {
            if (!IsAlive || amount <= 0f)
                return;

            CurrentHealth = Mathf.Max(CurrentHealth - amount, 0f);
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

            if (!IsAlive)
                Die();
        }

        public void Heal(float amount)
        {
            if (!IsAlive || amount <= 0f)
                return;

            float previous = CurrentHealth;
            CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);

            if (CurrentHealth > previous)
            {
                OnHeal?.Invoke(amount);
                OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
            }
        }

        protected virtual void Die()
        {
            OnDeath?.Invoke();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (maxHealth < 1f) maxHealth = 1f;
            if (regenRate < 0f) regenRate = 0f;
        }
#endif
    }
}
