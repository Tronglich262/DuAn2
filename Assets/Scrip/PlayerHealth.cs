using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false; // Kiểm tra nhân vật đã chết chưa

    public Slider healthBar;
    private Animator animator;
    private Player player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();

        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
        if (maxHealth <= 0) 
        {
            ScoreManager.Instance.ResetScore();
        }

       
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return; // Nếu đã chết thì không nhận sát thương

        Debug.Log("Va chạm với: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Bullet2"))
        {
            StartCoroutine(Hit());
            TakeDamage(2);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Bullet3"))
        {
            StartCoroutine(Hit());
            TakeDamage(20);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator Hit()
    {
        if (isDead) yield break; // Nếu đã chết thì không chạy animation bị đánh

        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Hit", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return; // Nếu đã chết thì không nhận sát thương

        if (other.gameObject.CompareTag("Bullet4"))
        {
            StartCoroutine(Hit());
            TakeDamage(50);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Hit());
            TakeDamage(2);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Nếu đã chết thì không trừ máu

        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return; // Nếu đã chết rồi thì không gọi lại

        isDead = true; // Đánh dấu nhân vật đã chết
        Debug.Log("Người chơi đã chết!");
        ScoreManager.Instance.ResetScore(); 

        // Dừng mọi hoạt động của nhân vật
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false; // Vô hiệu hóa va chạm

        // Chạy animation chết
        animator.SetBool("Die", true); // Giữ nhân vật ở animation chết
        StartCoroutine(ResetGame());
        Debug.Log("Thoi gian reset: " + ResetGame());


    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }
    public void IncreaseHealth(int amount)
    {
        if (currentHealth >= maxHealth)
        {
            Debug.Log("Máu đã đầy, không thể hồi thêm!");
            return;
        }

        int oldHealth = currentHealth;
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        int actualHeal = currentHealth - oldHealth;

        Debug.Log("Hồi máu: " + actualHeal + ", Máu hiện tại: " + currentHealth);
        UpdateHealthUI();

        // 🔥 Gọi Player kích hoạt animation buff
        if (player != null)
        {
            player.TriggerBuffAnimation();
        }
    }

    IEnumerator ResetGame()
    {
        yield  return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menu");
    }
    
}