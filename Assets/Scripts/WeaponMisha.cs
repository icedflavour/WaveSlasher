using UnityEngine;

public class WeaponMisha : MonoBehaviour
{
    public int AttackRange;
    public int DamageValue;
    public GameObject Projectile;

    public void PerformAttack()
    {
        var projectile = Instantiate(Projectile, transform.position, Quaternion.identity, transform);
        projectile.GetComponent<Rigidbody2D>().linearVelocityX = 3;
    }

}
