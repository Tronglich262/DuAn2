using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
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

    public Joystick joystick; // Thêm Joystick

    public Button jumpButton;
    public Button attackButton;
    public Button shieldButton;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Gán sự kiện cho button
        if (jumpButton != null) jumpButton.onClick.AddListener(Jump);
        if (attackButton != null) attackButton.onClick.AddListener(Attack);
        if (shieldButton != null) shieldButton.onClick.AddListener(Shield);
        
    }

    void Update()
    {
        float moveX = joystick.GetInput().x; // Lấy giá trị từ Joystick
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // Xét bool nếu nhân vật di chuyển
        animator.SetBool("walk", moveX != 0);

        // Kiểm tra hướng nhân vật để quay mặt
        if (moveX > 0 && !isFacingRight)
            Flip();
        else if (moveX < 0 && isFacingRight)    
            Flip();
    }

    public void Jump()
    {
       // if (isGrounded) // Chỉ nhảy nếu chạm đất
        //{
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetBool("Jump", true);
            Invoke(nameof(StopJump), 0.5f); // Dừng animation sau 0.5 giây
        //}
    }

    void StopJump()
    {
        animator.SetBool("Jump", false);
    }

    public void Attack()
    {
        animator.SetBool("guomattack", true);
        Invoke(nameof(StopAttack), 0.5f);
    }

    void StopAttack()
    {
        animator.SetBool("guomattack", false);
    }

    public void Shield()
    {
        animator.SetBool("khien", true);
        Invoke(nameof(StopShield), 1f);
    }

    void StopShield()
    {
        animator.SetBool("khien", false);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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
}
