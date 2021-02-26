using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public AudioClip destroySound;
    public float destroyDelay;
    public float impactForce;

    void Start()
    {
        Transform[] children = new Transform [transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }  
        
        transform.DetachChildren();

        // Add explosion force from center of object.
        foreach (Transform child in children)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb && impactForce != 0)
            {
                Vector3 pushDir = child.position - transform.position;
                rb.AddForce(pushDir * impactForce, ForceMode.Force);
                rb.AddTorque(Random.insideUnitSphere, ForceMode.Force);
            }
        }

        if (destroySound)
        {
            AudioSource.PlayClipAtPoint(destroySound, transform.position);
        }

        Destroy(gameObject, destroyDelay);
    }
}