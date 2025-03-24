using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
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
        // Kiểm tra xem Player có trong vùng giữa A & B không
        bool playerInZone = IsPlayerBetweenAandB();

        if (playerInZone)
        {
            isChasing = true; // Nếu Player trong vùng, quái sẽ tấn công
        }
        else
        {
            isChasing = false; // Nếu Player rời khỏi vùng, quái trở lại tuần tra
        }

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
        //animator.SetBool("isRunning", true);

        Transform target = movingToB ? pointB : pointA;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Xoay theo hướng di chuyển
        Flip(target.position.x);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            movingToB = !movingToB; // Đảo hướng khi đến nơi
        }
    }

    void ChasePlayer()
    {
       // animator.SetBool("isRunning", true);

        // Đuổi theo Player
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

        // Xoay theo hướng của Player
        Flip(player.position.x);

        // Nếu chạm vào Player thì thực hiện tấn công
        if (Vector2.Distance(transform.position, player.position) < stopDistance)
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack"); // Gọi animation tấn công
        Debug.Log("Quái tấn công Player!");

        // Thực hiện sát thương hoặc xử lý va chạm tại đây (gọi hàm từ Player)

        // Dừng di chuyển sau khi tấn công (nếu cần)
        StartCoroutine(RetreatAfterAttack());
    }
    IEnumerator RetreatAfterAttack()
    {
        float retreatTime = 0.3f; // Thời gian để lùi
        float retreatDistance = 3f; // Khoảng cách lùi (~2 pixel)

        Vector2 retreatDirection = (transform.position - player.position).normalized; // Hướng lùi
        Vector2 retreatTarget = (Vector2)transform.position + retreatDirection * retreatDistance; // Vị trí cần lùi đến

        float elapsedTime = 0;
        while (elapsedTime < retreatTime)
        {
            transform.position = Vector2.MoveTowards(transform.position, retreatTarget, (retreatDistance / retreatTime) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.3f); // Chờ 0.3 giây trước khi tấn công tiếp
    }


    bool IsPlayerBetweenAandB()
    {
        float minX = Mathf.Min(pointA.position.x, pointB.position.x);
        float maxX = Mathf.Max(pointA.position.x, pointB.position.x);

        return player.position.x > minX && player.position.x < maxX;
    }

    void Flip(float targetX)
    {
        // Nếu quái đang nhìn bên phải nhưng Player ở bên trái => Quay lại
        if ((targetX < transform.position.x && transform.localScale.x < 0) ||
            (targetX > transform.position.x && transform.localScale.x > 0))
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1; // Đảo hướng
            transform.localScale = newScale;
        }
    }
    
}
