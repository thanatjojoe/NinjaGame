using UnityEngine;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 1.0f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        if (enemyPrefab != null && GetComponent<Collider>() != null)
        {
            Collider collider = GetComponent<Collider>();

            while (true) 
            {
                Vector3 randomSpawnPoint = new Vector3(
                    Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                    collider.bounds.center.y,
                    Random.Range(collider.bounds.min.z, collider.bounds.max.z)
                );

                Instantiate(enemyPrefab, randomSpawnPoint, Quaternion.identity);

                // รอเวลาที่กำหนดระหว่างการ spawn ถัดไป
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }
}