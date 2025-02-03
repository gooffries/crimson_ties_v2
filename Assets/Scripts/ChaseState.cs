using UnityEngine;
using UnityEngine.AI;

public class ChaseState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("❌ Player object not found!");
        }

        agent.speed = 7f;
        // agent.stoppingDistance = 8f;  // ✅ AI will  stop 2 meters away instead of colliding
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, animator.transform.position);
        Debug.Log($"🚀 AI Distance to Player: {distance}");

        agent.SetDestination(player.position);
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (distance > 30f)
        {
            Debug.Log("🟡 AI TOO FAR, RETURNING TO PATROL!");
            animator.SetBool("isChasing", false);
            animator.SetBool("isPatrolling", true);
        }

        if (distance <= 12f)
        {
            Debug.Log("🛑 AI Stopped! Switching to ATTACK STATE.");
            agent.isStopped = true;
            animator.SetBool("isAttacking", true);
            animator.SetBool("isChasing", false);
        }
    }



    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.ResetPath(); // ✅ Stops movement without forcing position
    }
}
