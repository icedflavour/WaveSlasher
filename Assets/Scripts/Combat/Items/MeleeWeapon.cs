using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat.Items;
using Core;

public class MeleeWeapon : WeaponBase
{
[Header("Arc Settings")]
[Range(0f, 360f)] public float arcAngle;
public float arcRadius;
[Range(0f, 360f)] public float arcOffset;

[Header("Sweep Animation Settings")]
[Range(1, 50)] public int rayCount = 10;
[Range(0.01f, 1f)] public float thicknessPercent = 0.3f;
public float sweepDuration = 0.2f;

[Header("Animator")]
public Animator animator;

private float currentCenterAngle = 0f;
private bool isSweeping = false;
private float lastAttackTime = 0f;

protected override void PerformAttack(Vector2 direction)
{
    if (Time.time - lastAttackTime < 1f / stats.attackSpeed)
        return;

    lastAttackTime = Time.time;

    if (animator != null)
    {
        animator.ResetTrigger("AttackEnd");
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");
        animator.speed = 1f;
    }

    StartCoroutine(AttackRoutine(direction));
}

private IEnumerator AttackRoutine(Vector2 direction)
{
    yield return SweepArc(direction);
    if (animator != null)
    {
        animator.SetTrigger("AttackEnd");
    }
}

private IEnumerator SweepArc(Vector2 direction)
{
    float halfThickness = (thicknessPercent * arcAngle) * 0.5f;
    float elapsed = 0f;
    isSweeping = true;

    HashSet<IDamageable> hitThisSweep = new HashSet<IDamageable>();

    while (elapsed < sweepDuration)
    {
        elapsed += Time.deltaTime;
        float t = elapsed / sweepDuration;

        currentCenterAngle = Mathf.Lerp(-arcAngle / 2f, arcAngle / 2f, t);

        float minA = currentCenterAngle - halfThickness + arcOffset;
        float maxA = currentCenterAngle + halfThickness + arcOffset;

        SweepSegment(minA, maxA, hitThisSweep);

        yield return null;
    }

    isSweeping = false;
}

private void SweepSegment(float minAngle, float maxAngle, HashSet<IDamageable> hitThisSweep)
{
    for (int i = 0; i < rayCount; i++)
    {
        float lerp = (float)i / (rayCount - 1);
        float angle = Mathf.Lerp(minAngle, maxAngle, lerp);
        float rad = angle * Mathf.Deg2Rad;

        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        RaycastHit2D hit = Physics2D.Raycast(
            attackOrigin.position,
            dir,
            arcRadius,
            LayerMask.GetMask("Enemy")
        );

        if (hit.collider != null && hit.collider.TryGetComponent(out IDamageable dmg))
        {
            if (!hitThisSweep.Contains(dmg))
            {
                dmg.TakeDamage(stats.damage);
                hitThisSweep.Add(dmg);
            }
        }
    }
}

#if UNITY_EDITOR
private void OnDrawGizmos()
{
if (attackOrigin == null)
return;

    Vector3 origin = attackOrigin.position;
    float halfArc = arcAngle * 0.5f;
    float halfThickness = (arcAngle * thicknessPercent) * 0.5f;
    float center = isSweeping ? currentCenterAngle : 0f;

    float minAng = center - halfThickness + arcOffset;
    float maxAng = center + halfThickness + arcOffset;

    Gizmos.color = Color.yellow;
    DrawArc(origin, arcRadius, arcOffset - halfArc, arcOffset + halfArc);

    Gizmos.color = new Color(1f, 0.4f, 0f, 0.35f);
    DrawArc(origin, arcRadius * 0.95f, minAng, maxAng);

    Gizmos.color = Color.cyan;

    if (rayCount < 1) return;

    for (int i = 0; i < rayCount; i++)
    {
        float t = (float)i / (rayCount - 1);
        float angle = Mathf.Lerp(minAng, maxAng, t) * Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
        Vector3 end = origin + dir * arcRadius;
        Gizmos.DrawLine(origin, end);
    }
}

private void DrawArc(Vector3 center, float radius, float startAng, float endAng)
{
    int steps = 32;
    Vector3 prevPoint = Vector3.zero;
    bool hasPrev = false;

    for (int i = 0; i <= steps; i++)
    {
        float t = i / (float)steps;
        float ang = Mathf.Lerp(startAng, endAng, t) * Mathf.Deg2Rad;
        Vector3 point = center + new Vector3(Mathf.Cos(ang), Mathf.Sin(ang), 0f) * radius;

        if (hasPrev)
            Gizmos.DrawLine(prevPoint, point);

        prevPoint = point;
        hasPrev = true;
    }
}

#endif
}
