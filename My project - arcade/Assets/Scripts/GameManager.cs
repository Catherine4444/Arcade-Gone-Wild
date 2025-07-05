using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro; 


public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    private float enemySpawnRate = 2;

    [SerializeField] private List<GameObject> humans;
    public int numberOfHumansAlive = 1; // initial amount to be count down

    [SerializeField] private List<GameObject> powerups;
    public bool hasPowerUp;
    private float powerUpSpawnRate = 2;

    //[SerializeField] private timer;

    // for spawning, but mostly powerups 
    private float xRange = 16.8f;
    private float zRange = 5.5f;
    private float ySpawnPos = 0.5f;

    // for spawning enemies 
    Vector3 center = new Vector3(0, 0, 0);
    Vector3 size = new Vector3(30.4f, 0, 7.6f); // width = 10, height = 5
    float furthest = 1.75f;
    float closest = 0.8f;
    float halfWidth;
    float halfHeight;

    public bool gameNotOver;

    int wave = 0;
    internal double enemiesActive = 0;
    [SerializeField] private int[,] itemsToSpawn = new int[,]
    {
        {1, 0, 0},
        {2, 1, 0},
        {2, 0, 0},
        {3, 1, 0}
    }; //row is wave number, column 1 is at most enemy, column 2 is power up, column 3 is humans
    // fifth wave is set numbers of enemies , before 
    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasPowerUp = false;
        gameNotOver = true;
        //ObjectPoolManager.SpawnObject(powerups[index], RandomPowerupSpawnPosition(), powerups[index].transform.rotation, ObjectPoolManager.PoolType.Powerups);

        halfWidth = size.x / 2f;
        halfHeight = size.z / 2f;

        int index = UnityEngine.Random.Range(0, powerups.Count);
        ObjectPoolManager.SpawnObject(powerups[index], RandomPowerupSpawnPosition(), powerups[index].transform.rotation, ObjectPoolManager.PoolType.Powerups);
        
        SpawnHuman(numberOfHumansAlive);

        StartCoroutine(SpawnItems());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnHuman(int number = 1 )
    {
        int index = UnityEngine.Random.Range(0, humans.Count);
        //Instantiate(enemies[index], RandomEnemySpawnPosition(), enemies[index].transform.rotation);
        ObjectPoolManager.SpawnObject(humans[index], Human.spawnPos, humans[index].transform.rotation, ObjectPoolManager.PoolType.Humans);

    }

    

    IEnumerator SpawnItems()
    {
        double enemiesToSpawn;
        //double powerupsToSpawn;
        yield return new WaitForSeconds(5);
        int totalWaves = itemsToSpawn.GetLength(0);
        Debug.Log($"number of waves {totalWaves}");
        while (gameNotOver && (wave < totalWaves))
        {
            Debug.Log($"start of wave {wave}");
            SpawnPowerUp(itemsToSpawn[wave, 1]);
            yield return new WaitForSeconds(powerUpSpawnRate);

            if (wave < (totalWaves - 2))
            {
                enemiesToSpawn = UnityEngine.Random.Range(1, itemsToSpawn[wave, 0]+1);
            }
            else
            {
                Debug.Log("last wave");
                enemiesToSpawn = itemsToSpawn[wave, 0];
            }

            enemiesActive = enemiesToSpawn;
            SpawnEnemies(enemiesToSpawn);
            Debug.Log($"start of wave {wave}. Enemies to Spawn: {enemiesToSpawn}. enemies active: {enemiesActive}");
            // Wait until all enemies are destroyed
            yield return new WaitUntil(() => enemiesActive == 0);
            yield return new WaitForSeconds(enemySpawnRate);
            wave++;
        }
        GameOver();

    }

    void SpawnPowerUp(int powerupsToSpawn)
    {
         for (int i = 0; i < powerupsToSpawn; i++)
            {
                int index = UnityEngine.Random.Range(0, powerups.Count);
                ObjectPoolManager.SpawnObject(powerups[index], RandomPowerupSpawnPosition(), powerups[index].transform.rotation, ObjectPoolManager.PoolType.Powerups);
            }
    }

    void SpawnEnemies(double enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            //Debug.Log($"enemy spawn loop {i}");
            int index = UnityEngine.Random.Range(0, enemies.Count); // determine a random enemy to spawn 
            ObjectPoolManager.SpawnObject(enemies[index], RandomEnemySpawnPosition(), enemies[index].transform.rotation);
        }
        //Debug.Log($"Enenies Active {gameManager.enemiesActive}");
    }



    Vector3 RandomEnemySpawnPosition()
    {

        // Choose a side: 0 = left, 1 = right, 2 = top, 3 = bottom
        int side = UnityEngine.Random.Range(0, 4);
        Vector3 spawnPos = new Vector3(0, ySpawnPos, 0);

        switch (side)
        {
            case 0: // Left
                spawnPos.x = center.x - halfWidth - UnityEngine.Random.Range(closest, furthest);
                spawnPos.z = UnityEngine.Random.Range(center.z - halfHeight, center.z + halfHeight);
                break;
            case 1: // Right
                spawnPos.x = center.x + halfWidth + UnityEngine.Random.Range(closest, furthest);
                spawnPos.z = UnityEngine.Random.Range(center.z - halfHeight, center.z + halfHeight);
                break;
            // want the top to be decorative 
            case 2: // Top
                spawnPos.z = center.z + halfHeight + UnityEngine.Random.Range(closest, furthest);
                spawnPos.x = UnityEngine.Random.Range(center.x - halfWidth, center.x + halfWidth);
                break;
            case 3: // Bottom
                spawnPos.z = center.z - halfHeight - UnityEngine.Random.Range(closest, furthest);
                spawnPos.x = UnityEngine.Random.Range(center.x - halfWidth, center.x + halfWidth);
                break;
        }

        return spawnPos;
    }

    Vector3 RandomPowerupSpawnPosition()
    {
        return new Vector3(UnityEngine.Random.Range(-xRange,xRange), ySpawnPos, UnityEngine.Random.Range(-zRange,zRange));
    }

    public void GameOver()
    {
        gameNotOver = false;
        Debug.Log("Game Over");
    }

    public void PowerUpAquired()
    {
        hasPowerUp = true;
        Debug.Log("powerup");
        StartCoroutine(PowerupCountdown());
    }

    private IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(10);
        hasPowerUp = false;

    }

    public void Print(String s)
    {
        Debug.Log(s);
    }
}
