using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    
    public GameObject lootPrefab; // Prefab của viên đạn hoặc vật phẩm
    public float spawnInterval = 3f; // Thời gian giữa mỗi lần spawn
    public float fallSpeed = 5f; // Tốc độ rơi
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SpawnItemRoutine());
    }

    IEnumerator SpawnItemRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            GameObject spawnedItem = Instantiate(lootPrefab, transform.position, Quaternion.Euler(0, 0, -90));
            Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();

            if (rb == null)
            {
                rb = spawnedItem.AddComponent<Rigidbody2D>(); // Nếu chưa có Rigidbody2D thì thêm vào
            }

            rb.gravityScale = 1; // Đảm bảo có trọng lực để rơi xuống
            rb.linearVelocity = new Vector2(0, -fallSpeed); // Tạo vận tốc rơi ngay lập tức
        }
    }
}
