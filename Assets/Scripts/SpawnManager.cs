using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public  GameObject[] powerUpPrefab;
    public GameObject bossPrefab;

    public List<GameObject> inSceneEnemy = new List<GameObject>();

    private float spawnX = 9.0f;
    private float spawnZ = 9.0f;

    public int enemyCount;
    private int waveNumber = 1;

    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        SpawnEnemyWave(waveNumber);
    }

    void Update()
    {
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;

        if (enemyCount == 0 && !playerControllerScript.gameOver)
        {
            inSceneEnemy.Clear();
            waveNumber++;
            if((waveNumber % 5) != 0)
            {
                SpawnEnemyWave(waveNumber);
            }
            else
            {
                BossBattle();
            }
        }
    }

     private void SpawnEnemyWave(int waveNumber)
    {
        for (int i=0; i< waveNumber; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefab.Length);
            GameObject newEnemy = Instantiate(enemyPrefab[enemyIndex], GenerateSpawnPosition(), enemyPrefab[enemyIndex].transform.rotation);
            inSceneEnemy.Add(newEnemy);
        }
        PowerUpSpawn();
    }

    private void BossBattle()
    {
        GameObject BossEnemy = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        inSceneEnemy.Add(BossEnemy);
        for (int i = 0; i < 2; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab[1], GenerateSpawnPosition(), enemyPrefab[1].transform.rotation);
            inSceneEnemy.Add(newEnemy);
        }
        PowerUpSpawn();
    }

    private void PowerUpSpawn()
    {
        int powerUpIndex = Random.Range(0, powerUpPrefab.Length);
        GameObject newPowerUp = Instantiate(powerUpPrefab[powerUpIndex], GenerateSpawnPosition(), powerUpPrefab[powerUpIndex].transform.rotation);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnX, spawnX);
        float spawnPosZ = Random.Range(-spawnZ, spawnZ);

        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnPos;
    }
}