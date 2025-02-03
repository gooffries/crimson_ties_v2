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
    private int currentHealth;

    public GameObject healthBarPrefab; // ✅ Assign Health Bar Prefab in Inspector
    private Slider healthBar;
    private Transform healthBarTransform;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        if (healthBarPrefab != null)
        {
            // ✅ Instantiate health bar and attach it above the guard
            GameObject healthBarInstance = Instantiate(healthBarPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
            healthBarTransform = healthBarInstance.transform;
            healthBar = healthBarInstance.GetComponentInChildren<Slider>();

            // ✅ Attach the health bar to follow the enemy
            healthBarTransform.SetParent(null);
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
            // ✅ Make the health bar always face the camera
            healthBarTransform.position = transform.position + Vector3.up * 2;
            healthBarTransform.LookAt(Camera.main.transform);
        }

        if (isAttacking && agent != null && agent.isOnNavMesh)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > attackRange)
            {
                agent.SetDestination(player.position);
                // animator.SetBool("isAttacking", false);
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

    public void TakeDamage(int damage)
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
        Destroy(healthBarTransform.gameObject); // ✅ Remove health bar
        Destroy(gameObject, 2f); // ✅ Remove guard after 2 sec
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }
}
