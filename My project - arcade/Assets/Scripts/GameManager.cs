using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemies;
    private float enemySpawnRate = 4;

    public List<GameObject> humans;
    public int numberOfHumansAlive = 1; // initial amount to be count down

    public List<GameObject> powerups;
    public bool hasPowerUp = false;
    private float powerUpSpawnRate = 10;

    private float xRange = 16.8f;
    private float zRange = 5.5f;
    private float ySpawnPos = 0.5f;

    public bool gameNotOver = true;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnHuman(numberOfHumansAlive);

        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnHuman(int number = 1 )
    {
        int index = Random.Range(0, humans.Count);
        //Instantiate(enemies[index], RandomEnemySpawnPosition(), enemies[index].transform.rotation);
        ObjectPoolManager.SpawnObject(humans[index], Human.spawnPos, humans[index].transform.rotation, ObjectPoolManager.PoolType.Humans);

    }

    

    IEnumerator SpawnEnemy()
    {
        while (gameNotOver)
        {
            yield return new WaitForSeconds(enemySpawnRate);
            int index = Random.Range(0, enemies.Count);
            //Instantiate(enemies[index], RandomEnemySpawnPosition(), enemies[index].transform.rotation);
            ObjectPoolManager.SpawnObject(enemies[index], RandomEnemySpawnPosition(), enemies[index].transform.rotation);
        }
    }

    IEnumerator SpawnPowerUp()
    {
        while (gameNotOver)
        {
            int index = Random.Range(0, enemies.Count);
            //Instantiate(powerups[index], RandomPowerupSpawnPosition(), powerups[index].transform.rotation);
            ObjectPoolManager.SpawnObject(powerups[index], RandomPowerupSpawnPosition(), powerups[index].transform.rotation, ObjectPoolManager.PoolType.Powerups);
            yield return new WaitForSeconds(powerUpSpawnRate);
        }
    }

    Vector3 RandomEnemySpawnPosition()
    {
        return new Vector3(Random.Range(-xRange,xRange), ySpawnPos, Random.Range(-zRange,zRange));
    }

    Vector3 RandomPowerupSpawnPosition()
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
