using UnityEngine;
using System.Collections;

public class LightningSpawner : MonoBehaviour
{
    public GameObject lightningPrefab; // Prefab sét
    public float minSpawnTime = 1f; // Thời gian nhỏ nhất giữa các lần xuất hiện
    public float maxSpawnTime = 2f; // Thời gian lớn nhất giữa các lần xuất hiện
    public float lightningLifetime = 0.5f; // Thời gian sét tồn tại

    public Vector2 spawnAreaMin; // Góc trái dưới của vùng spawn
    public Vector2 spawnAreaMax; // Góc phải trên của vùng spawn

    void Start()
    {
        StartCoroutine(SpawnLightning());
    }

    IEnumerator SpawnLightning()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            // Chọn vị trí random trong vùng spawn
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(randomX, randomY);

            // Tạo sét tại vị trí ngẫu nhiên
            GameObject lightning = Instantiate(lightningPrefab, spawnPosition, Quaternion.identity);

            // Xóa sét sau một thời gian ngắn
            Destroy(lightning, lightningLifetime);
        }
    }
}