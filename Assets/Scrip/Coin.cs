using UnityEngine;
using TMPro; // Import để sử dụng TextMeshPro

public class Coin : MonoBehaviour
{
    public int coinValue = 5; // Giá trị của coin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kiểm tra va chạm với Player
        {
            ScoreManager.Instance.AddScore(coinValue); // Cộng điểm
            Destroy(gameObject); // Xóa coin
        }
    }
}