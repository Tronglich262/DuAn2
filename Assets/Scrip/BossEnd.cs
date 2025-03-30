using System.Collections;
using UnityEngine;

public class BossEnd : MonoBehaviour
{
    public Transform pointA, pointB, player;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float stopDistance = 1.5f;
    public float attackCooldown = 1f; // Thời gian giữa các lần tấn công
    public float retreatDistance = 3f; // Lùi lại 3 pixel
    public float retreatTime = 0.1f; // Thời gian lùi lại

    private bool isChasing = false;
    private bool isAttacking = false;
    private bool movingToB = true;
    private bool isCollidingWithPlayer = false;
    public GameObject healthBarUI; 

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        if (healthBarUI != null)
        {
            healthBarUI.SetActive(false); // Ẩn thanh máu khi bắt đầu
        }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isAttacking) return; // Đang tấn công thì không di chuyển

        if (IsPlayerBetweenAandB())
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= stopDistance)
            {
                isChasing = false;
                if (!isAttacking)
                {
                    StartCoroutine(AttackLoop());
                }
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (isAttacking) return;

        animator.SetBool("Running", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);

        Transform target = movingToB ? pointB : pointA;
        transform.position = Vector2.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);
        Flip(target.position.x);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            movingToB = !movingToB;
            animator.SetBool("Running", false);
            animator.SetBool("Idle", true);
        }
    }

    void ChasePlayer()
    {
        if (isAttacking) return;

        animator.SetBool("Running", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);

        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        Flip(player.position.x);
    }

    IEnumerator AttackLoop()
    {
        isAttacking = true;
        animator.SetBool("Running", false);
        animator.SetBool("Idle", false);

        while (isCollidingWithPlayer) 
        {
            animator.SetBool("Attack", true);
            Debug.Log("Boss đang tấn công Player!");

            yield return new WaitForSeconds(attackCooldown);

            yield return StartCoroutine(RetreatAfterAttack()); // Lùi lại sau mỗi lần tấn công
        }

        animator.SetBool("Attack", false);
        isAttacking = false;
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
        if ((targetX < transform.position.x && transform.localScale.x > 0) ||
            (targetX > transform.position.x && transform.localScale.x < 0))
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player vào vùng tấn công!");
            isCollidingWithPlayer = true;
            if (healthBarUI != null)
            {
                healthBarUI.SetActive(true); // Hiện thanh máu khi phát hiện Player
            }
            if (!isAttacking)
            {
                StartCoroutine(AttackLoop());
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player rời khỏi vùng tấn công!");
            isCollidingWithPlayer = false;
            if (healthBarUI != null)
            {
                healthBarUI.SetActive(false); // Ẩn thanh máu khi Player rời đi
            }
        }
    }
}
