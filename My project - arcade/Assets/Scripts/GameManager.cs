using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> humans;
    private float enemySpawnRate = 4;

    private float xRange = 16.8f;
    private float zRange = 5.5f;
    private float ySpawnPos = 0.5f;

    public bool gameNotOver = true;
    public int numberOfHumansAlive = 1;

    public bool hasPowerUp = false;
    private float powerUpSpawnRate = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while (gameNotOver)
        {
            yield return new WaitForSeconds(enemySpawnRate);
            int index = Random.Range(0, enemies.Count);
            Instantiate(enemies[index], RandomEnemySpawnPosition(), enemies[index].transform.rotation);
        }
    }

    IEnumerator SpawnPowerUp()
    {
        while (gameNotOver)
        {
            int index = Random.Range(0, enemies.Count);
            Instantiate(enemies[index], RandomEnemySpawnPosition(), enemies[index].transform.rotation);
            yield return new WaitForSeconds(powerUpSpawnRate);
        }
    }

    Vector3 RandomEnemySpawnPosition()
    {
        return new Vector3(Random.Range(-xRange,xRange), ySpawnPos, Random.Range(-zRange,zRange));
    }

    public void GameOver()
    {
        gameNotOver = false;
        Debug.Log("Game Over");
    }

    public void PowerUpAquired()
    {
        hasPowerUp = true;
    }
}
