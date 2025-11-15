using UnityEngine;

namespace Combat.Items
{
    public class RangedWeapon : WeaponBase
    {
        protected override void PerformAttack(Vector2 direction)
        {
            SpawnProjectile(direction);
        }
    }
}
