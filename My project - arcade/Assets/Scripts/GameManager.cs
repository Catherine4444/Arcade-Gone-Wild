using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro; 


public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    private float enemySpawnRate = 10;

    [SerializeField] private List<GameObject> humans;
    public int numberOfHumansAlive = 1; // initial amount to be count down

    [SerializeField] private List<GameObject> powerups;
    public bool hasPowerUp = false;
    private float powerUpSpawnRate = 10;

    //[SerializeField] private timer;

    private float xRange = 16.8f;
    private float zRange = 5.5f;
    private float ySpawnPos = 0.5f;

    public bool gameNotOver = true;

    int wave = 1;
    double enemiesToSpawn = 1;
    internal double enemiesActive = 0;
    [SerializeField] private int[][] itemsToSpawn; //row is wave number, column 1 is enemy, column 2 is power up, column 3 is humans
    
    
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
        int index = UnityEngine.Random.Range(0, humans.Count);
        //Instantiate(enemies[index], RandomEnemySpawnPosition(), enemies[index].transform.rotation);
        ObjectPoolManager.SpawnObject(humans[index], Human.spawnPos, humans[index].transform.rotation, ObjectPoolManager.PoolType.Humans);

    }

    

    IEnumerator SpawnEnemy()
    {
        //enemiesActive = enemiesToSpawn;
        //while (gameNotOver)
        //{
            //if (enemiesActive <= 0)
            //{
                yield return new WaitForSeconds(enemySpawnRate);
                
                enemiesActive = enemiesToSpawn;
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    int index = UnityEngine.Random.Range(0, enemies.Count);
                    //Instantiate(enemies[index], RandomEnemySpawnPosition(), enemies[index].transform.rotation);
                    ObjectPoolManager.SpawnObject(enemies[index], RandomEnemySpawnPosition(), enemies[index].transform.rotation);
                }

                // Wait until all enemies are destroyed
                yield return new WaitUntil(() => enemiesActive == 0);
                

                wave++;
                
                enemiesToSpawn = 2*UnityEngine.Random.Range(1,wave);

                if (gameNotOver)
                {
                    StartCoroutine(SpawnEnemy());
                }
                

            //}
            
        //}

    }

    IEnumerator SpawnPowerUp()
    {
        while (gameNotOver)
        {

            for (int i = 0; i < wave; i++)
            {
                int index = UnityEngine.Random.Range(0, enemies.Count);
                //Instantiate(powerups[index], RandomPowerupSpawnPosition(), powerups[index].transform.rotation);
                ObjectPoolManager.SpawnObject(powerups[index], RandomPowerupSpawnPosition(), powerups[index].transform.rotation, ObjectPoolManager.PoolType.Powerups);
            }

            yield return new WaitForSeconds(powerUpSpawnRate);
        }
    }

    Vector3 RandomEnemySpawnPosition()
    {
        // Vector3 spawnPosition = 
        // if (Vector3.Distance(spawnPosition, player.position) >= minDistanceFromPlayer)
        //     {
        //         Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        //         spawned++;
        //     }

        //return new Vector3(UnityEngine.Random.Range(-xRange,xRange), ySpawnPos, UnityEngine.Random.Range(-zRange,zRange));
        Vector3 center = new Vector3(0, 0, 0);
        Vector3 size = new Vector3(10, 0, 5); // width = 10, height = 5

        float halfWidth = size.x / 2f;
        float halfHeight = size.y / 2f;

        // Choose a side: 0 = left, 1 = right, 2 = top, 3 = bottom
        int side = Random.Range(0, 4);
        Vector2 spawnPos = Vector2.zero;

        switch (side)
        {
            case 0: // Left
                spawnPos.x = center.x - halfWidth - UnityEngine.Random.Range(1f, 5f);
                spawnPos.y = UnityEngine.Random.Range(center.y - halfHeight, center.y + halfHeight);
                break;
            case 1: // Right
                spawnPos.x = center.x + halfWidth + UnityEngine.Random.Range(1f, 5f);
                spawnPos.y = UnityEngine.Random.Range(center.y - halfHeight, center.y + halfHeight);
                break;
            // want the top to be decorative 
            // case 2: // Top
            //     spawnPos.y = center.y + halfHeight + UnityEngine.Random.Range(1f, 5f);
            //     spawnPos.x = UnityEngine.Random.Range(center.x - halfWidth, center.x + halfWidth);
            //     break;
            case 3: // Bottom
                spawnPos.y = center.y - halfHeight - UnityEngine.Random.Range(1f, 5f);
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
    }
}
