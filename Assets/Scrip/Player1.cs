using UnityEngine;
using UnityEngine.UI;

public class Player1 : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 25f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isGrounded;
    public int count = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // Nhận giá trị từ bàn phím
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // Cập nhật trạng thái di chuyển cho animator
        if (animator != null)
            animator.SetBool("walk", moveX != 0);

        // Kiểm tra hướng nhân vật để quay mặt
        if (moveX > 0 && !isFacingRight)
            Flip();
        else if (moveX < 0 && isFacingRight)
            Flip();

        // Kiểm tra nếu nhấn Space để nhảy
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            Jump();
        }

        // Các hành động khác
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Shield();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerBuffAnimation();
        }

        // Mở túi đồ
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false; // Đặt lại trạng thái nhảy
        if (animator != null)
        {
            animator.SetBool("Jump", true);
        }
        Invoke(nameof(StopJump), 0.5f);
    }

    void StopJump()
    {
        if (animator != null)
        {
            animator.SetBool("Jump", false);
        }
    }

    public void Attack()
    {
        if (animator != null)
        {
            animator.SetBool("guomattack", true);
            Invoke(nameof(StopAttack), 0.5f);
        }
    }

    void StopAttack()
    {
        if (animator != null)
        {
            animator.SetBool("guomattack", false);
        }
    }

    public void Shield()
    {
        if (animator != null)
        {
            animator.SetBool("khien", true);
            Invoke(nameof(StopShield), 1f);
        }
    }

    void StopShield()
    {
        if (animator != null)
        {
            animator.SetBool("khien", false);
        }
    }

    public void TriggerBuffAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("buff", true);
            Invoke(nameof(StopBuffAnimation), 1f);
        }
    }

    void StopBuffAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("buff", false);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f); // Quay nhân vật thay vì đổi `localScale`
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void ToggleInventory()
    {
        Debug.Log("Mở túi đồ");
        // Thêm code để bật/tắt UI túi đồ
    }
}
