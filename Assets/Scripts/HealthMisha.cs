using UnityEngine;

public class HealthMisha : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;

    public void TakeDamage(int damageValue)
    {
        CurrentHealth -= damageValue;
        if (CurrentHealth < 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
