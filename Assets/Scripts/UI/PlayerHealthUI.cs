using UnityEngine;
using TMPro;
using Core;

namespace UI
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [Header("UI Reference")]
        [SerializeField] private TextMeshProUGUI healthText;

        private HealthController playerHealth;

        private void Start()
        {
            // Find the player in registry
            if (PlayerRegistry.Player == null)
            {
                Debug.LogError("[PlayerHealthUI] PlayerRegistry.Player is null!");
                return;
            }

            // Get HealthController from player
            playerHealth = PlayerRegistry.Player.GetComponent<HealthController>();

            if (playerHealth == null)
            {
                Debug.LogError("[PlayerHealthUI] Player has no HealthController!");
                return;
            }

            // Subscribe to events
            playerHealth.OnHealthChanged += UpdateHealthText;
            playerHealth.OnDeath += HandlePlayerDeath;

            // Initialize UI
            UpdateHealthText(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        }

        private void OnDestroy()
        {
            // Unsubscribe to avoid memory leaks
            if (playerHealth != null)
            {
                playerHealth.OnHealthChanged -= UpdateHealthText;
                playerHealth.OnDeath -= HandlePlayerDeath;
            }
        }

        /// <summary>
        /// Updates the HP UI.
        /// </summary>
        private void UpdateHealthText(float current, float max)
        {
            if (healthText != null)
                healthText.text = $"Health: {current} / {max}";
        }

        /// <summary>
        /// Called when the player dies.
        /// </summary>
        private void HandlePlayerDeath()
        {
            if (healthText != null)
                healthText.text = "Health: 0 / " + playerHealth.MaxHealth;
        }
    }
}
