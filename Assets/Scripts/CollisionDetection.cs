using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wp;
    public GameObject HitParticle;

    private bool hasSpawnedEffect = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && wp.IsAttacking && !hasSpawnedEffect)
        {
            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");
            Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);

            hasSpawnedEffect = true; // Prevent further spawning
            Invoke(nameof(ResetEffectSpawn), 0.5f); // Reset after 0.5 seconds
        }
    }

    private void ResetEffectSpawn()
    {
        hasSpawnedEffect = false;
    }
}
