using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : StateMachineBehaviour
{
    float timer;
    List<Transform> waypoints = new List<Transform>();
    NavMeshAgent agent;
    Transform player;
    float chaseRange = 12f; // ✅ Increased range to detect player earlier

    // OnStateEnter is called when the enemy starts patrolling
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("❌ Player not found! Make sure the player has the 'Player' tag.");
            return;
        }

        agent = animator.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("❌ NavMeshAgent is missing on enemy!");
            return;
        }

        timer = 0;
        GameObject[] waypointsObjects = GameObject.FindGameObjectsWithTag("WayPoints");

        if (waypointsObjects.Length == 0)
        {
            Debug.LogError("❌ No waypoints found! Ensure waypoints have the 'WayPoints' tag.");
            return;
        }

        foreach (GameObject obj in waypointsObjects)
        {
            waypoints.Add(obj.transform);
        }

        if (waypoints.Count > 0)
        {
            agent.SetDestination(waypoints[Random.Range(0, waypoints.Count)].position);
        }
    }

    // OnStateUpdate runs every frame while patrolling
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null) return; // ✅ Prevents errors if player is missing

        // ✅ Check if player is within range & immediately chase if detected
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < chaseRange)
        {
            Debug.Log("👀 Player detected! Switching to Chase.");
            animator.SetBool("isChasing", true);
            return; // ✅ Immediately exit patrol update
        }

        // ✅ Continue patrolling only if no player is detected
        if (agent.remainingDistance <= agent.stoppingDistance && waypoints.Count > 0)
        {
            agent.SetDestination(waypoints[Random.Range(0, waypoints.Count)].position);
        }

        timer += Time.deltaTime;
        if (timer > 10)
        {
            animator.SetBool("isPatrolling", false);
        }
    }

    // OnStateExit is called when leaving the patrol state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null)
        {
            agent.isStopped = true; // ✅ Stop the agent properly before transitioning
            agent.ResetPath(); // ✅ Clear path to prevent navigation issues
        }
    }
}
