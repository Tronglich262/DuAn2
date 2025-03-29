using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Kiem : MonoBehaviour
{
    public int damage = 10;
    public BoxCollider2D swordCollider;
    public GameObject damageTextPrefab;
    public Button attackButton; // Thêm Button tấn công

    private void Start()
    {
        swordCollider.enabled = false; // Tắt collider lúc đầu

        // Gán sự kiện cho Button
        if (attackButton != null)
        {
            attackButton.onClick.AddListener(Attack);
        }
    }

    private void Update()
    {
        // Kiểm tra nếu nhấn phím Q thì thực hiện Attack
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }

    public void Attack()
    {
        StartCoroutine(CHO());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Kiểm tra nếu va chạm với quái
        {
            Vector3 spawnPos = other.transform.position + new Vector3(0, 1, 0);

            // Tạo popup damage ngay tại vị trí quái
            GameObject dmgPopup = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);
            dmgPopup.GetComponent<DamagePopup>().Setup(damage);

            // Gọi hàm để trừ máu quái
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Destroy(other.gameObject);
            }

            Debug.Log("Chém trúng quái!");
        }
    }

    IEnumerator CHO()
    {
        yield return new WaitForSeconds(0.3f);
        swordCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        swordCollider.enabled = false;
    }
}
