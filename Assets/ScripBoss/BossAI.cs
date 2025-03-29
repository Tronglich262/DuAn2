using UnityEngine;

public class BossAl : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public Animator animator;

    private enum State { MovingToA, MovingToB, Attacking }
    private State currentState;
    private Vector3 target;
    private bool isAttacking = false;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        currentState = State.MovingToB;
        target = pointB.position;
        Flip(target);

        if (animator != null)
            animator.SetBool("Jump", true);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (isAttacking) return; // Dừng di chuyển nếu đang tấn công

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            SwitchState();
        }
    }

    void SwitchState()
    {
        if (currentState == State.MovingToA)
        {
            currentState = State.MovingToB;
            target = pointB.position;
        }
        else
        {
            currentState = State.MovingToA;
            target = pointA.position;
        }

        Flip(target);

        if (animator != null)
            animator.SetBool("Jump", true);
    }

    void Flip(Vector3 targetPosition)
    {
        Vector3 scale = transform.localScale;
        scale.x = (targetPosition.x > transform.position.x) ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    // Khi Player vào vùng điểm A hoặc B
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Phát hiện Player - Bắt đầu tấn công!");
            isAttacking = true;
            currentState = State.Attacking;

            if (animator != null)
            {
                animator.SetBool("Running", false);
                animator.SetTrigger("Attack"); // Sử dụng Trigger thay vì Bool
                animator.SetBool("Damaged", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player rời khỏi - Tiếp tục di chuyển!");
            isAttacking = false;

            if (animator != null)
            {
                animator.SetBool("Attack", false);
                animator.SetBool("Damaged", false);
                animator.SetBool("Running", true);
            }

            target = (currentState == State.MovingToA) ? pointA.position : pointB.position;
        }
    }
}
