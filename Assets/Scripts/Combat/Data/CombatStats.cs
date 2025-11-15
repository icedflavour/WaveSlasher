using UnityEngine;

namespace Combat.Data
{
    [CreateAssetMenu(fileName = "CombatStats", menuName = "Combat/Combat Stats", order = 0)]
    public class CombatStats : ScriptableObject
    {
        [Header("Base Stats")]
        [Tooltip("Base damage dealt by the weapon or ability.")]
        public float damage = 10f;

        [Tooltip("Attacks per second. A higher value means faster attack rate.")]
        public float attackSpeed = 1f;

        [Tooltip("Maximum range of the attack or projectile.")]
        public float range = 5f;

        [Tooltip("Speed of the projectile (if applicable).")]
        public float projectileSpeed = 10f;

        [Header("AOE & Bounce")]
        [Tooltip("Radius of the splash area for area-of-effect attacks.")]
        public float splashRadius = 0f;

        [Tooltip("Number of bounces or pierces before destruction.")]
        public int bounceCount = 0;

        [Header("Targeting")]
        [Tooltip("If true, attack will auto-target the nearest enemy.")]
        public bool isTargeted = false;

        [Tooltip("If true, this is a melee-type attack.")]
        public bool isMelee = false;

        [Header("Projectile Prefab")]
        [Tooltip("Prefab used for projectile-based weapons.")]
        public GameObject projectilePrefab;

        [Header("Visual & Sound")]
        [Tooltip("Optional attack effect or particle system.")]
        public GameObject attackVFX;

        [Tooltip("Optional sound to play during attack.")]
        public AudioClip attackSFX;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (damage < 0f) damage = 0f;
            if (attackSpeed <= 0f) attackSpeed = 0.1f;
            if (range < 0f) range = 0f;
            if (projectileSpeed < 0f) projectileSpeed = 0f;
            if (splashRadius < 0f) splashRadius = 0f;
            if (bounceCount < 0) bounceCount = 0;
        }
#endif
    }
}
