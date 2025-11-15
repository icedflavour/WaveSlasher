namespace Core
{
    public interface IDamageable
    {
        /// <summary>
        /// Apply damage to this object.
        /// </summary>
        /// <param name="amount">Amount of damage to apply.</param>
        void TakeDamage(float amount);

        /// <summary>
        /// Apply healing to this object.
        /// </summary>
        /// <param name="amount">Amount of healing to apply.</param>
        void Heal(float amount);

        /// <summary>
        /// Whether this object is alive.
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// Current health value.
        /// </summary>
        float CurrentHealth { get; }

        /// <summary>
        /// Maximum health value.
        /// </summary>
        float MaxHealth { get; }
    }
}
