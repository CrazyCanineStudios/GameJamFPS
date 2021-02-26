using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGenerator : MonoBehaviour
{
    public static EnemyGenerator instance;
    public GameObject enemyPrefab;
    public List<SpawnZone> spawnZones = new List<SpawnZone>();

    public float spawnDelay = 5.0f;
    private float spawnTimer = 0.0f;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnDelay && spawnZones.Count > 0)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    public void AddZone(SpawnZone zone)
    {
        spawnZones.Add(zone);
    }

    private void SpawnEnemy()
    {
        SpawnZone zone = spawnZones[Random.Range(0, spawnZones.Count)];
        GameObject enemy = Instantiate(enemyPrefab, FindSpawnPoint(zone.transform, zone.radius), Quaternion.identity);
        enemy.transform.SetParent(transform);
    }

    private Vector3 FindSpawnPoint(Transform contextTransform, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += contextTransform.position;

        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1))
            finalPosition = hit.position;

        return randomDirection;
    }
}
