using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private HealthBar healthBar; // ✅ Reference to the built-in HealthBar
    private HealthBarManager healthBarManager;

    public Animator animator;

    public NavMeshAgent agent;

    private bool isDead = false; // ✅ Prevents multiple calls to Die()

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError($"❌ Animator component is missing on {gameObject.name}!");
        }

        currentHealth = maxHealth;

        // ✅ Find the HealthBarManager
        healthBarManager = FindObjectOfType<HealthBarManager>();

        if (healthBarManager == null)
        {
            Debug.LogError("❌ HealthBarManager not found in the scene!");
        }
        else
        {
            // ✅ Request a health bar from HealthBarManager
            healthBarManager.AddHealthBar(gameObject, maxHealth);
        }

        // ✅ Try to find the health bar after requesting one
        healthBar = GetComponentInChildren<HealthBar>();

        if (healthBar == null)
        {
            Debug.LogError($"❌ CRITICAL: {gameObject.name} STILL has NO HealthBar! Check HealthBarManager.");
        }
        else
        {
            Debug.Log($"✅ HealthBar successfully assigned for {gameObject.name}.");
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return; // ✅ Ignore non-player collisions

        Debug.Log($"⚠️ Enemy {gameObject.name} collided with {other.name}");

        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            float attackDamage = 20f;
            player.TakeDamage(attackDamage);
            Debug.Log($"⚔ Enemy hit Player for {attackDamage} damage!");
        }
    }


    public void TakeDamage(float damage)
    {
        Debug.Log($"⚔️ {gameObject.name} took {damage} damage.");

        if (animator == null)
        {
            Debug.LogError($"❌ Animator not found on {gameObject.name}! Assign it in Inspector.");
            return; // ✅ Prevents further execution if animator is missing
        }

        if (currentHealth <= 0)
        {
            Debug.Log($"⚠️ {gameObject.name} is already dead! Ignoring further damage.");
            return; // ✅ Prevent taking damage after death
        }

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        // ✅ Prevent multiple calls if health is already zero
        if (currentHealth == 0 && animator.GetBool("isDead"))
        {
            Debug.Log($"⚠️ {gameObject.name} is already dead! Skipping redundant death logic.");
            return;
        }

        Debug.Log($"⚔️ {gameObject.name} new health: {currentHealth}/{maxHealth}");

        // ✅ Ensure health bar updates properly
        if (healthBar != null)
        {
            Debug.Log("🩸 Updating health bar inside enemy...");
            healthBar.UpdateHealth(currentHealth);
        }
        else
        {
            Debug.LogError("❌ healthBar is NULL in Enemy.cs!");
        }

        if (healthBarManager != null)
        {
            Debug.Log("🩸 Updating health bar in HealthBarManager...");
            healthBarManager.UpdateHealthBar(gameObject, currentHealth);
        }
        else
        {
            Debug.LogWarning("⚠️ HealthBarManager not found in Enemy!");
        }

        if (currentHealth <= 0)
        {
            Debug.Log("💀 Enemy is dying!");
            Die(); // ✅ Immediately trigger death
        }
        else
        {
            Debug.Log("🩸 Enemy still alive, playing hit animation.");
            animator.SetTrigger("damage"); // ✅ Play "hit" animation only if still alive
            StartCoroutine(RecoverFromHit()); // ✅ Allow AI to resume normal states
        }
    }



    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float healthPercentage = Mathf.Clamp01(currentHealth / maxHealth); // Normalize health value
            healthBar.UpdateHealth(currentHealth);
            Debug.Log($"🩸 {gameObject.name} HealthBar Updated: {healthPercentage * 100}%");
        }
        else
        {
            Debug.LogWarning("⚠ HealthBar is NOT assigned in Enemy prefab!");
        }
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(0.5f); // ✅ Short delay after hit
        animator.SetBool("isChasing", true); // ✅ Resume chasing or attacking
    }

    private void Die()
    {
        if (isDead) return; // ✅ Prevents repeated execution
        isDead = true;
        Debug.Log($"💀 {gameObject.name} is dying...");

        // ✅ Stop enemy movement before destroying
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.isStopped = true;
        }

        // ✅ Play death animation
        if (animator != null)
        {
            animator.SetTrigger("die");
        }

        // ✅ Remove health bar
        if (healthBarManager != null)
        {
            Debug.Log("🩸 Removing health bar...");
            healthBarManager.RemoveHealthBar(gameObject);
        }

        // ✅ Wait for animation to finish before destroying
        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        float deathAnimationDuration = 2.0f; // Default wait time if animation is missing

        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsTag("Death")) // ✅ Ensure we are playing a valid death animation
            {
                deathAnimationDuration = stateInfo.length;
            }
        }

        yield return new WaitForSeconds(deathAnimationDuration); // ✅ Wait for animation to end

        // ✅ Drop rewards AFTER animation finishes
        EnemyDeathHandler deathHandler = GetComponent<EnemyDeathHandler>();
        if (deathHandler != null)
        {
            deathHandler.HandleDeath();
        }
        else
        {
            Debug.LogWarning($"⚠ EnemyDeathHandler not found for {gameObject.name}!");
        }

        // ✅ Destroy enemy after effects have finished
        Destroy(gameObject);
    }
}