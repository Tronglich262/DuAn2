using UnityEngine;

public class EnemyShooting : MonoBehaviour
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

            // Xác định hướng bắn theo flipX
            Vector2 shootDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;
            float rotationAngle = spriteRenderer.flipX ? 180f : 0f;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, rotationAngle));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Destroy(bullet, 3f);

            rb.linearVelocity = shootDirection * bulletSpeed;
        }
    }
}