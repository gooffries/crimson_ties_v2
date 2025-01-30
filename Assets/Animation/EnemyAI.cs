using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform pointA;   // First patrol point
    public Transform pointB;   // Second patrol point
    public Transform player;   // Player reference
    public float attackRange = 5f; // Distance to chase player

    private NavMeshAgent agent;
    private Transform targetPoint; // Current patrol target
    private bool isChasingPlayer = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetPoint = pointA; // Start moving towards point A
        agent.SetDestination(targetPoint.position);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // Player is in range, chase them
            isChasingPlayer = true;
            agent.SetDestination(player.position);
        }
        else
        {
            // Player is out of range, go back to patrolling
            if (isChasingPlayer)
            {
                isChasingPlayer = false;
                agent.SetDestination(targetPoint.position);
            }

            // Continue patrolling if not chasing
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                SwitchPatrolTarget();
            }
        }
    }

    void SwitchPatrolTarget()
    {
        // Switch between point A and point B
        targetPoint = (targetPoint == pointA) ? pointB : pointA;
        agent.SetDestination(targetPoint.position);
    }
}
