using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword;
    public bool CanAttack = true;
    public float AttackCoolDown = 1.0f;

    public AudioClip SwordAttackSound;
    public bool IsAttacking = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanAttack)
        {
            Debug.Log("Mouse Click Detected!");
            SwordAttack();
        }
    }


    public void SwordAttack()
    {
        IsAttacking = true;
        Debug.Log("Attack Triggered!");
        CanAttack = false; // Prevent spamming attacks
        Animator anim = Sword.GetComponent<Animator>();


        anim.SetTrigger("Attack"); // Trigger attack animation immediately
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(SwordAttackSound);

        StartCoroutine(ResetAttackCoolDown());
    }


    IEnumerator ResetAttackCoolDown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCoolDown);
        CanAttack = true;
        Debug.Log("Cooldown finished. Ready to attack.");
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        IsAttacking = true;

    }
}
