using UnityEngine;

public abstract class WeaponBase : IDamage
{
    public abstract void Attack();
    protected virtual void OnHitTarget(IDamageable target)
    {
        if (target != null)
        {
        }
    }


}
