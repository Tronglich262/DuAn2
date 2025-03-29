using System.Collections;
using UnityEngine;

public class BossEnd : MonoBehaviour
{
    public Transform pointA; // Điểm A (vị trí tuần tra)
    public Transform pointB; // Điểm B (vị trí tuần tra)
    public Transform player; // Player

    public float speed = 2f; // Tốc độ tuần tra
    public float chaseSpeed = 4f; // Tốc độ đuổi theo Player
    public float stopDistance = 0.5f; // Khoảng cách dừng lại khi gần Player

    private bool movingToB = true; // Kiểm tra hướng di chuyển
    private bool isChasing = false; // Kiểm tra trạng thái tấn công

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool playerInZone = IsPlayerBetweenAandB();
        isChasing = playerInZone;

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        animator.SetBool("Running", true); // Bật animation chạy
        Transform target = movingToB ? pointB : pointA;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        Flip(target.position.x);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            movingToB = !movingToB; // Đảo hướng khi đến nơi
        }
    }

    void ChasePlayer()
    {
        animator.SetBool("Running", true); // Bật animation chạy
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        Flip(player.position.x);

        if (Vector2.Distance(transform.position, player.position) < stopDistance)
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetBool("Running", false); // Dừng animation chạy
        animator.ResetTrigger("Attack"); // Reset trước để tránh bị bỏ qua
        animator.SetTrigger("Attack"); // Sử dụng Trigger để kích hoạt animation
        Debug.Log("Quái tấn công Player!");

        StartCoroutine(RetreatAfterAttack());
    }


    IEnumerator RetreatAfterAttack()
    {
        animator.SetBool("Running", true); // Tiếp tục chạy khi lùi
        float retreatTime = 0.3f;
        float retreatDistance = 3f;

        Vector2 retreatDirection = (transform.position - player.position).normalized;
        Vector2 retreatTarget = (Vector2)transform.position + retreatDirection * retreatDistance;

        float elapsedTime = 0;
        while (elapsedTime < retreatTime)
        {
            transform.position = Vector2.MoveTowards(transform.position, retreatTarget,
                (retreatDistance / retreatTime) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
    }

    bool IsPlayerBetweenAandB()
    {
        float minX = Mathf.Min(pointA.position.x, pointB.position.x);
        float maxX = Mathf.Max(pointA.position.x, pointB.position.x);
        return player.position.x > minX && player.position.x < maxX;
    }

    void Flip(float targetX)
    {
        if ((targetX < transform.position.x && transform.localScale.x > 0) ||
            (targetX > transform.position.x && transform.localScale.x < 0))
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }
}