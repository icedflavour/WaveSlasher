using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public Slider targetHealthBar;
    public float damage = 20f;
    public float speed = 10f;

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        if (targetHealthBar == null)
        {
            targetHealthBar = target.GetComponentInChildren<Slider>();
        }
    }

    void Update()
    {
        if (target == null || targetHealthBar == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            ApplyDamage();
        }
    }

    void ApplyDamage()
    {
        targetHealthBar.value -= damage;

        if (targetHealthBar.value <= 0)
        {
            Destroy(target.gameObject);
        }

        Destroy(gameObject);
    }
}