using UnityEngine;
using Core;

namespace Enemy
{
    [RequireComponent(typeof(HealthController))]
    public class EnemyDeathController: MonoBehaviour
    {
        private HealthController health;

        private void Awake()
        {
            health = GetComponent<HealthController>();
            health.OnDeath += HandleDeath;
        }

        private void OnDestroy()
        {
            if (health != null)
                health.OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            // TODO: add animation, SFX, loot, etc.
            Destroy(gameObject);
        }
    }
}
