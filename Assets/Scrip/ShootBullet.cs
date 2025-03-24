using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab viên đạn
    public Transform firePoint; // Vị trí bắn
    public float bulletSpeed = 30f; // Tốc độ viên đạn
    private bool facingRight = true; // Kiểm tra hướng nhân vật

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) // Nhấn "A" để bắn
        {
            Shoot();
        }

        // Lấy hướng di chuyển (giả sử bạn có Input)
        float moveDirection = Input.GetAxisRaw("Horizontal");
        if (moveDirection != 0)
        {
            FlipCharacter(moveDirection);
        }
    }

    void Shoot()
    {
        // Xác định hướng bắn theo hướng nhân vật
        Vector2 shootDirection = facingRight ? Vector2.right : Vector2.left;

        // Tạo viên đạn tại firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Gán vận tốc cho viên đạn theo hướng bắn
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = shootDirection * bulletSpeed;

        // Xoay viên đạn theo hướng bắn
        float angle = facingRight ? 0 : 180;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Hủy đạn sau 3 giây
        Destroy(bullet, 3f);
    }

    void FlipCharacter(float moveDirection)
    {
        if (moveDirection > 0 && !facingRight)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
            firePoint.localScale = new Vector3(1, 1, 1);
        }
        else if (moveDirection < 0 && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
            firePoint.localScale = new Vector3(-1, 1, 1);
        }
    }
}