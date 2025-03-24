using UnityEngine;

public class EnemyShootingTrai : MonoBehaviour
{
    public Transform gunTransform;  // Gán vị trí súng vào đây
    public GameObject bulletPrefab;
    public Transform firePoint;  // Điểm bắn đạn
    public float bulletSpeed = 5f;
    public float fireRate = 2f; // Tốc độ bắn mỗi 2 giây

    private float nextFireTime;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        RotateGun();
        Shoot();
    }

    void RotateGun()
    {
        if (spriteRenderer.flipX) // Nếu quái quay trái
        {
            gunTransform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else // Nếu quái quay phải
        {
            gunTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Shoot()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            // Đảo ngược hướng bắn
            float direction = spriteRenderer.flipX ? 1f : -1f; // Đổi dấu
            float rotationAngle = spriteRenderer.flipX ? 0f : 180f; // Đổi góc quay

            // Tạo đạn
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, rotationAngle));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Destroy(bullet, 3f);

            // Cập nhật vận tốc đạn để bắn ngược lại
            rb.linearVelocity = new Vector2(bulletSpeed * direction, 0);
        }
    }


}