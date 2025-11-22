namespace Core
{
    public interface IDamageable
    {
        /// <summary>
        /// Отримати шкоду від джерела.
        /// </summary>
        /// <param name="amount">Кількість шкоди</param>
        void TakeDamage(float amount);

        /// <summary>
        /// Отримати лікування.
        /// </summary>
        /// <param name="amount">Кількість хіллу</param>
        void Heal(float amount);

        /// <summary>
        /// Чи живий об’єкт.
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// Поточне здоров’я.
        /// </summary>
        float CurrentHealth { get; }

        /// <summary>
        /// Максимальне здоров’я.
        /// </summary>
        float MaxHealth { get; }
    }
}
