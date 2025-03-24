using UnityEngine;
using System.Collections;

public class BossAL : MonoBehaviour
{
    public Transform pointA; // Điểm A
    public Transform pointB; // Điểm B
    public float moveSpeed = 5f; // Tốc độ di chuyển
    public float jumpForce = 10f; // Lực nhảy
    public float attackRange = 5f; // Phạm vi tấn công
    public LayerMask playerLayer; // Layer của người chơi

    private Animator animator;
    private Rigidbody rb;
    private Transform targetPoint; // Điểm đích hiện tại
    private bool isMoving = true;
    private bool isAttacking = false;
    private bool isJumping = false; // Thêm biến kiểm tra nhảy

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        targetPoint = pointB; // Bắt đầu di chuyển tới điểm B
        StartCoroutine(ChangeMovement()); // Bắt đầu coroutine thay đổi di chuyển
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }

        CheckForPlayer();
    }

    void MoveToTarget()
    {
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Kiểm tra nếu gần tới điểm đích
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Đổi điểm đích
            targetPoint = (targetPoint == pointB) ? pointA : pointB;
        }

        // Set animation Running
        animator.SetBool("Running", true);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        isJumping = true;
        StartCoroutine(ResetJump()); // Bắt đầu coroutine reset nhảy
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(1f); // Đợi 1 giây (hoặc thời gian animation nhảy)
        isJumping = false;
    }

    void CheckForPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, playerLayer);

        if (hitColliders.Length > 0 && !isAttacking)
        {
            isAttacking = true;
            isMoving = false; // Dừng di chuyển
            animator.SetBool("Running", false); // Dừng animation chạy

            // Tấn công
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack_Punch");

        // Sau khi animation tấn công kết thúc, gọi hàm ResetAttack
        Invoke("ResetAttack", animator.GetCurrentAnimatorStateInfo(0).length);
    }

    void ResetAttack()
    {
        isAttacking = false;
        isMoving = true; // Tiếp tục di chuyển
    }

    IEnumerator ChangeMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f)); // Đợi một khoảng thời gian ngẫu nhiên

            if (!isAttacking && !isJumping) // Chỉ thay đổi nếu không tấn công và không nhảy
            {
                if (Random.value < 0.5f) // 50% cơ hội nhảy
                {
                    Jump();
                }
            }
        }
    }
}