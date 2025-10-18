using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MovementController
{
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if (agent != null)
            agent.SetDestination(target.position);
    }

    private void Update()
    {
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
