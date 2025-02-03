using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GuardAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private bool isAttacking = false;

    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;

    public Animator animator;
    public int maxHealth = 100;
    private float currentHealth;

    public GameObject healthBarPrefab; // ✅ Assign Health Bar Prefab in Inspector
    private Slider healthBar;
    private Transform healthBarTransform;
    private GameObject healthBarInstance;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        if (healthBarPrefab != null)
        {
            Debug.Log($"🛠️ Spawning health bar for {gameObject.name}...");

            // ✅ Instantiate health bar OUTSIDE the enemy first
            healthBarInstance = Instantiate(healthBarPrefab);
            healthBarTransform = healthBarInstance.transform;
            healthBar = healthBarInstance.GetComponentInChildren<Slider>();

            // ✅ Set correct size BEFORE parenting to enemy (prevents scaling issues)
            healthBarTransform.localScale = Vector3.one;

            // ✅ Move the health bar above the guard’s head
            healthBarTransform.position = transform.position + Vector3.up * 3.5f;

            // ✅ Parent the health bar AFTER positioning it (avoids unwanted scaling)
            healthBarTransform.SetParent(transform, true);
        }
        else
        {
            Debug.LogError($"❌ ERROR: HealthBar Prefab is not assigned for {gameObject.name}!");
        }
    }




    void Update()
    {
        if (healthBarTransform != null)
        {
            // ✅ Move the health bar above the enemy (adjust height if needed)
            healthBarTransform.position = transform.position + Vector3.up * 3.5f;
        }

        if (isAttacking && agent != null && agent.isOnNavMesh)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > attackRange)
            {
                agent.SetDestination(player.position);
                animator.SetBool("isAttacking", false);
            }
            else
            {
                agent.isStopped = true;
                AttackPlayer();
            }
        }
    }

    public void StartAttacking()
    {
        if (agent == null || !agent.isOnNavMesh)
        {
            Debug.LogError($"❌ ERROR: {gameObject.name} cannot attack because it is NOT on a NavMesh!");
            return;
        }

        Debug.Log($"⚔️ {gameObject.name} is now attacking!");
        isAttacking = true;
        agent.isStopped = false;
    }

    private void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log($"💥 {gameObject.name} swings sword at player!");
            animator.SetBool("isAttacking", true);
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log($"☠️ {gameObject.name} has died!");
        animator.SetTrigger("Die");
        agent.isStopped = true;

        // ✅ Destroy health bar when guard dies
        if (healthBarInstance != null)
        {
            Destroy(healthBarInstance);
        }

        Destroy(gameObject, 2f);
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }
}
