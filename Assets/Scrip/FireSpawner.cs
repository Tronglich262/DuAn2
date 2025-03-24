using UnityEngine;
using System.Collections;

public class FireSpawner : MonoBehaviour
{
    public GameObject firePrefab; // Prefab ngọn lửa
    public float minSpawnTime = 3f;
    public float maxSpawnTime = 7f;
    public float fireLifetime = 1.5f;

    private float minX = -27f;  // Giới hạn X trái
    private float maxX = 12f;    // Giới hạn X phải
    private float spawnY = -7f; // Vị trí Y cố định trên mặt đất

    void Start()
    {
        StartCoroutine(SpawnFire());
    }

    IEnumerator SpawnFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            // Random vị trí X trong vùng giới hạn
            float randomX = Random.Range(minX, maxX);
            Vector2 spawnPosition = new Vector2(randomX, spawnY);

            // Tạo ngọn lửa
            GameObject fire = Instantiate(firePrefab, spawnPosition, Quaternion.identity);

            // Xóa sau một thời gian
            Destroy(fire, fireLifetime);
        }
    }
}