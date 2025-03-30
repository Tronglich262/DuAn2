using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Máu tối đa
    private float currentHealth;    // Máu hiện tại

    public Slider healthBar;        // Thanh máu UI
    public Transform healthBarUI;   // Đối tượng chứa thanh máu (Canvas nhỏ trên đầu Boss)
    public Vector3 offset = new Vector3(0, 2f, 0); // Điều chỉnh vị trí thanh máu
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;  // Bắt đầu với full máu
        UpdateHealthBar();
    }

    void Update()
    {
        if (healthBarUI != null)
        {
            healthBarUI.position = transform.position + offset; // Thanh máu di chuyển theo Boss
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Kiem") || collision.CompareTag("Khien")) // Nếu bị tấn công
        {
                animator.SetBool("Damaged", true);  // Gọi animation bị đánh
                TakeDamage(10); // Trừ máu
        }
        else
        {
            animator.SetBool("Damaged", false);
        }
    }
    



    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Giữ trong khoảng [0, maxHealth]
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth; // Cập nhật thanh máu
        }
    }

    void Die()
    {
        Debug.Log("Boss bị tiêu diệt!");
        StartCoroutine(DelayDie());
    }

    IEnumerator DelayDie()
    {
        animator.SetBool("Dead", true);
    
        yield return new WaitForSeconds(2f); // Chờ animation chết phát xong

        if (healthBarUI != null)
        {
            Destroy(healthBarUI.gameObject); // Xóa thanh máu khi Boss chết
        }
    
        Destroy(gameObject); // Xóa Boss sau khi animation kết thúc
    }

}
