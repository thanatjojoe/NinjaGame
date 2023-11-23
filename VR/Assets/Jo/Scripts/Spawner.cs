using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 3f; 
    public int maxEnemies = 10; 
    private Collider spawnCollider; 

    void Start()
    { 
        spawnCollider = GetComponent<Collider>();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        //spawn enemy ใน colliderของ เรา
        while (true)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
            {
                Vector3 spawnPosition = GetRandomPointOnCollider(spawnCollider);
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }

    
    Vector3 GetRandomPointOnCollider(Collider collider)
    {
        float u = Random.Range(0f, 1f);
        float v = Random.Range(0f, 1f);

        Vector3 randomPoint = collider.bounds.min + new Vector3(u * collider.bounds.size.x, 0f, v * collider.bounds.size.z);

        return randomPoint;
    }
}