using UnityEngine;

public class ProjectileMeleeWeapon : MonoBehaviour
{
    private WeaponType selectedOption;

    public enum WeaponType
    {
        Projectile,
        Melee
    }

    private void Attack()
    {   

    }

    void Start()
    {
        if(selectedOption == WeaponType.Melee)
        {
            Attack();
        }
    }
}