using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawntroilai : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 7 loại quái vật
    public Transform[] spawnPoints; // Vị trí spawn

    private Dictionary<int, Vector3> enemySpawnPositions = new Dictionary<int, Vector3>();
    private Dictionary<int, GameObject> enemyInstances = new Dictionary<int, GameObject>();

    void Start()
    {
        SpawnAllEnemies();
    }

    private void OnEnable()
    {
        Enemy.EnemyDied += RespawnEnemy;
    }

    private void OnDisable()
    {
        Enemy.EnemyDied -= RespawnEnemy;
    }

    void SpawnAllEnemies()
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            Vector3 spawnPosition = spawnPoints[i].position;
            GameObject newEnemy = Instantiate(enemyPrefabs[i], spawnPosition, Quaternion.identity);
            
            enemySpawnPositions[i] = spawnPosition;
            enemyInstances[i] = newEnemy;
            
            newEnemy.GetComponent<Enemy>().enemyID = i;
        }
    }

    void RespawnEnemy(int enemyID)
    {
        StartCoroutine(RespawnAfterDelay(enemyID, 3f));
    }

    IEnumerator RespawnAfterDelay(int enemyID, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (enemyInstances.ContainsKey(enemyID))
        {
            GameObject enemy = enemyInstances[enemyID];
            enemy.transform.position = enemySpawnPositions[enemyID]; // Đưa về vị trí cũ
            enemy.SetActive(true); // Bật lại quái vật
        }
    }
}