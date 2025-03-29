using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Khien : MonoBehaviour
{
    public int damage = 10;
    public BoxCollider2D swordCollider;
    public Button shieldButton; // Thêm Button khiên

    private void Start()
    {
        swordCollider.enabled = false; // Tắt collider lúc đầu

        // Gán sự kiện cho Button
        if (shieldButton != null)
        {
            shieldButton.onClick.AddListener(ActivateShield);
        }
    }

    private void Update()
    {
        // Kiểm tra nếu nhấn phím W thì kích hoạt khiên
        if (Input.GetKeyDown(KeyCode.W))
        {
            ActivateShield();
        }
    }

    public void ActivateShield()
    {
        StartCoroutine(CHO());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Kiểm tra nếu va chạm với quái
        {
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
