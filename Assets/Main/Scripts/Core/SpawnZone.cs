using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public float radius = 1;
    private bool isAdded = false;

    private void Awake()
    {
        radius = GetComponent<SphereCollider>().radius;
    }

    private void LateUpdate()
    {
        if (!isAdded && EnemyGenerator.instance != null)
        {
            EnemyGenerator.instance.AddZone(this);
            isAdded = true;
        }
    }
}
