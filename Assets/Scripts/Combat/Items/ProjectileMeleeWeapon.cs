using UnityEngine;

public class ProjectileMeleeWeapon : MonoBehaviour
{
    public void Attack();

    public enum WeaponType
    {
        Projectile,
        Melee
    }

    void Start()
    {
        if(selectedOption == Option.Melee)
        {
            void Attack()
            {
                
            }
        }
    }
}
