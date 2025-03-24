using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject itemPrefab;  // Prefab vật phẩm
    public float spawnInterval = 0.3f; // Thời gian giữa các lần spawn
    public float minX = -8f, maxX = 8f; // Giới hạn vị trí ngẫu nhiên theo trục X
    public float spawnHeight = 5f; // Độ cao so với Spawner

    void Start()
    {
        InvokeRepeating("SpawnItem", 0f, spawnInterval);
    }

    void SpawnItem()
    {
        float randomX = Random.Range(minX, maxX); // Tạo vị trí X ngẫu nhiên
        float spawnY = transform.position.y + spawnHeight; // Nâng cao vị trí spawn
        Vector2 spawnPosition = new Vector2(randomX, spawnY);
        
        // Tạo vật phẩm tại vị trí ngẫu nhiên và cao hơn
        Instantiate(itemPrefab, spawnPosition, Quaternion.Euler(0f, 0f, -90f));
    }
}