using UnityEngine;
using TMPro;
using Core;
using UnityEngine.UI;

namespace UI
{
    public class EnemyHealthUI : MonoBehaviour
    {
        [Header("UI Reference")]
        [SerializeField] private Slider HealthBarUI; 
        [SerializeField] private HealthController enemyHealth;

        private void Start()
        {
            //enemyHealth = GetComponentInParent<HealthController>();

            if (enemyHealth == null)
            {
                Debug.LogError("[EnemyHealthUI] Enemy has no HealthController!");
                return;
            }

            if (HealthBarUI != null)
            {
                HealthBarUI.maxValue = enemyHealth.MaxHealth;
                HealthBarUI.value    = enemyHealth.CurrentHealth;
            }

            // Subscribe to events
            enemyHealth.OnHealthChanged += UpdateHealthValue;
            enemyHealth.OnDeath += HandleEnemyDeath;

             
            // Initialize UI
            UpdateHealthValue(enemyHealth.CurrentHealth, enemyHealth.MaxHealth);
        }

        private void OnDestroy()
        {
            // Unsubscribe to avoid memory leaks
            if (enemyHealth != null)
            {
                enemyHealth.OnHealthChanged -= UpdateHealthValue;
                enemyHealth.OnDeath -= HandleEnemyDeath;
            }
        }

        /// <summary>
        /// Updates the HP UI.
        /// </summary>
        private void UpdateHealthValue(float current, float max)
        {
            if (HealthBarUI != null)
                HealthBarUI.value = current;
        }

        /// <summary>
        /// Called when the player dies.
        /// </summary>
        private void HandleEnemyDeath()
        {
            if (HealthBarUI != null)
                Destroy(gameObject);
        }

    }

}
