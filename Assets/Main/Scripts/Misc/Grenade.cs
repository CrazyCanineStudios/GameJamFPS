using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Rigidbody rb;
    private Transform mainCam;
    private bool isCountingDown;

    [SerializeField] private float forwardForce = 10f;
    [SerializeField] private float upwardsForce = 4f;
    [SerializeField] private float timeTillExplode = 1.5f;

    [SerializeField] private GameObject explosionPrefab = null;
    [SerializeField] private float explosionRadius = 3f;

    private bool hasExploded;

    void Start()
    {
        mainCam = Camera.main.transform;
        rb = this.GetComponent<Rigidbody>();

        // Throw the grenade up in the air.
        rb.AddForce((mainCam.forward * forwardForce) + (mainCam.up * upwardsForce), ForceMode.Impulse);
    }

    private void Update()
    {
        if (isCountingDown && timeTillExplode > 0)
        {
            timeTillExplode -= Time.deltaTime;
        }
        else if (timeTillExplode <= 0 && hasExploded == false)
        {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Environment"))
        {
            // If the grenade has landed on the ground then start counting down.
            isCountingDown = true;
        }
        else if (hasExploded == false)
        {
            // if the grenade has hit something else e.g. enemy or crate then explode immediately.
            Explode();
        }
    }
    private void Explode()
    {
        hasExploded = true;

        // Create an explosion particle effect.
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = this.transform.position;

        // Check for targets to apply damage to.
        CheckForTargets();

        // Destroy the grenade.
        Destroy(this.gameObject, 1f);
    }
    private void CheckForTargets()
    {
        // Get all targets inside the explosion radius.
        Collider[] targets = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < targets.Length; i++)
        {
            // Apply damage if they have health. TODO Kenneth - EntityHealth not yet implemented.
            if (targets[i].TryGetComponent(out CharacterMaster target))
            {
                Debug.Log("Apply Damage to target");
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (hasExploded)
        {
            // Draw the explosion radius for debugging
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}
