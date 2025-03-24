using UnityEngine;

public class EnemyHealth: MonoBehaviour
{
    public int damageToPlayer = 5; // Sát thương khi chạm vào Player

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra va chạm với Player
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer); // Trừ máu Player
                Debug.Log("Enemy tấn công Player! -5 HP");
            }
        }
    }
}