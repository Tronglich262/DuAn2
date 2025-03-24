using UnityEngine;

public class HiddenPlatformBay : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer; // Nếu muốn ẩn luôn hình ảnh

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Nếu có SpriteRenderer
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player chạm vào, ẩn nền!");

            // Ẩn Collider để nhân vật rơi xuống
            boxCollider.enabled = false;

            // Ẩn luôn hình ảnh nếu muốn
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
        }
    }
}