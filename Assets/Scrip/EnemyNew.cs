using UnityEngine;

public class EnemyNew : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 target;
    private Transform enemyTransform;
    private bool movingToB = true; // Xác định hướng di chuyển

    void Start()
    {
        enemyTransform = transform;
        target = pointB.position;
    }

    void Update()
    {
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, target, speed * Time.deltaTime);

        // Kiểm tra nếu quái đến gần điểm đích thì đổi hướng
        if (Vector3.Distance(enemyTransform.position, target) < 0.1f)
        {
            movingToB = !movingToB;
            target = movingToB ? pointB.position : pointA.position;
        }

        // Xoay mặt theo hướng di chuyển
        Flip();
    }

    void Flip()
    {
        // Kiểm tra hướng di chuyển để quay đúng
        if (target.x > enemyTransform.position.x)
        {
            enemyTransform.localScale = new Vector3(1, 1, 1); // Quay mặt sang phải
        }
        else
        {
            enemyTransform.localScale = new Vector3(-1, 1, 1); // Quay mặt sang trái
        }
    }
}