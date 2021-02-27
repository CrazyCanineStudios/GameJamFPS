using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1;

    private void FixedUpdate()
    {
        transform.position -= transform.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != CharacterMaster.instance)
            Destroy(gameObject);
    }
}
