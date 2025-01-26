using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword; // Reference to the sword GameObject
    public bool CanAttack = true; // Tracks whether the player can attack
    public float AttackCoolDown = 1.0f; // Time between attacks
    private float currentSwordStrength = 1f; // Current sword strength
    public AudioClip SwordAttackSound; // Sound to play during attack
    public bool IsAttacking = false;

    void Start()
    {
        // Initialize sword strength from UIManager
        if (UIManager.Instance != null)
        {
            currentSwordStrength = UIManager.Instance.GetSwordStrength();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanAttack)
        {
            SwordAttack();
        }
    }

    public void SwordAttack()
    {
        IsAttacking = true;
        CanAttack = false;

        // Trigger sword attack animation
        Animator anim = Sword.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }

        // Play sword attack sound
        AudioSource ac = GetComponent<AudioSource>();
        if (ac != null && SwordAttackSound != null)
        {
            ac.PlayOneShot(SwordAttackSound);
        }

        StartCoroutine(ResetAttackCoolDown());
    }

    public void UpdateSwordStrength(float newStrength)
    {
        currentSwordStrength = newStrength;
        Debug.Log($"Sword strength updated to: {currentSwordStrength}");
    }

    public float GetSwordDamage()
    {
        // Calculate sword damage based on current strength
        return currentSwordStrength;
    }

    IEnumerator ResetAttackCoolDown()
    {
        yield return new WaitForSeconds(AttackCoolDown);
        CanAttack = true;
    }
}
