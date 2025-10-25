using UnityEngine;
using UnityEngine.AI;
using Core;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MovementController
{
    private Transform target;
    private Vector3 lastTargetPosition;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (target == null)
            target = PlayerRegistry.Player;

        if (agent == null || target == null || !agent.isOnNavMesh)
            return;

        if ((Vector3)target.position != lastTargetPosition)
        {
            agent.SetDestination(target.position);
            lastTargetPosition = target.position;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        if (agent != null && target != null && agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
            lastTargetPosition = target.position;
        }
    }

    public override void Stop()
    {
        if (agent != null)
            agent.ResetPath();
        else
            base.Stop();
    }
}
