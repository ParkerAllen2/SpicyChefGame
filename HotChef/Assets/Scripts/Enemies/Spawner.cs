using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate;
    public float nextSpawn = 0;
    public Vector2 topright;

    private void Update()
    {
        if (GameController.Instance.gameOver)
        {
            return;
        }
        if (nextSpawn < Time.time)
        {
            nextSpawn = Time.time + spawnRate;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 position = new Vector3(
            Random.Range(-topright.x, topright.x),
            Random.Range(-topright.y, topright.y)
            );
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }
}
