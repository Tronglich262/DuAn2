using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletDestroyBridge : MonoBehaviour
{
    private Tilemap bridgeTilemap;
    private TilemapCollider2D bridgeCollider;

    void Start()
    {
        GameObject bridgeObject = GameObject.FindWithTag("Bridge"); // Tìm Tilemap theo tag
        if (bridgeObject != null)
        {
            bridgeTilemap = bridgeObject.GetComponent<Tilemap>(); 
            bridgeCollider = bridgeObject.GetComponent<TilemapCollider2D>(); // Lấy TilemapCollider2Ds
        }
        else
        {
            Debug.LogError("Không tìm thấy Tilemap có tag 'Bridge'!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bridge") && bridgeTilemap != null)
        {
            Vector3 hitPosition = collision.contacts[0].point; 
            Vector3Int tilePosition = bridgeTilemap.WorldToCell(hitPosition);

            // **Xóa ô tile chính giữa**
            if (bridgeTilemap.HasTile(tilePosition))
            {
                bridgeTilemap.SetTile(tilePosition, null);
            }

            // **Cập nhật lại collider sau khi xóa tile**
            if (bridgeCollider != null)
            {
                bridgeCollider.enabled = false;
                bridgeCollider.enabled = true;
            }

            Destroy(gameObject); // Hủy viên đạn
        }
    }
}