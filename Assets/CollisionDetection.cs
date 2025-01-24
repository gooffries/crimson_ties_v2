using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wp;
    public GameObject HitParticle;

    public float splashSize = 0.5f; // Adjust the splash size dynamically

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && wp.IsAttacking)
        {
            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("Hit");

            GameObject particle = Instantiate(HitParticle,
                new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z),
                other.transform.rotation);

            // Adjust the particle's size dynamically
            var particleSystem = particle.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                var main = particleSystem.main;
                main.startSize = splashSize; // Set the desired splash size
            }
        }
    }
}
