using UnityEngine;
using UnityEngine.AI; // ✅ Required for NavMeshAgent
using System.Collections;

public class AttackState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent; // ✅ Declare NavMeshAgent
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("⚔ AI ENTERED ATTACK STATE!");

        // ✅ Assign NavMeshAgent CORRECTLY
        agent = animator.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("❌ ERROR: NavMeshAgent is STILL NULL! AI GameObject might not have it.");
            return;
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("❌ ERROR: Player object not found! Make sure the player is tagged correctly.");
            return;
        }

        Debug.Log($"✅ NavMeshAgent FOUND: {agent.gameObject.name}");
        agent.isStopped = true; // ✅ Stop AI movement while attacking
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
        {
            Debug.LogError("❌ `player` is NULL! Did you tag the Player correctly?");
            return;
        }

        if (agent == null)
        {
            Debug.LogError("❌ `agent` is NULL! Did you assign NavMeshAgent in `OnStateEnter()`?");
            return;
        }

        float distance = Vector3.Distance(player.position, animator.transform.position);

        // ✅ Keep AI facing the player while attacking
        Vector3 direction = (player.position - animator.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRotation, Time.deltaTime * 5f);

        if (distance > 20f) // ✅ If player moves away, chase again
        {
            Debug.Log("🏃 Player moved away! Stop attacking & chase!");
            animator.SetBool("isAttacking", false);
            animator.SetBool("isChasing", true);
            agent.isStopped = false; // Resume movement
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isAttacking", false); // ✅ Reset attack
        animator.SetBool("isChasing", true); // ✅ Resume chasing if player is near
    }

}
