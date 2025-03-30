using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public enum ItemType
    {
        Gold500,
        Health50,
        ActivateMap
    }

    public ItemType itemType;
    public int itemPrice; // Giá vật phẩm
    public Button buyButton;
    public GameObject item; // Dùng cho button kích hoạt bản đồ nếu có

    void Start()
    {
        if (buyButton != null)
        {
            buyButton.onClick.AddListener(BuyItem);
        }

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateButtonState;
        }

        UpdateButtonState();
    }

    void BuyItem()
    {
        if (ScoreManager.Instance != null && ScoreManager.Instance.SpendScore(itemPrice))
        {
            Debug.Log("Mua thành công! Giá: " + itemPrice);

            switch (itemType)
            {
                case ItemType.Gold500:
                    ScoreManager.Instance.AddScore(500);
                    break;

                case ItemType.Health50:
                    PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.IncreaseHealth(50);
                    }
                    break;

                case ItemType.ActivateMap:
                    if (item != null)
                    {
                        item.SetActive(true); // Kích hoạt bản đồ
                    }
                    break;
            }

            UpdateButtonState();
        }
        else
        {
            Debug.Log("Không đủ vàng để mua.");
        }
    }

    void UpdateButtonState()
    {
        if (buyButton != null && ScoreManager.Instance != null)
        {
            buyButton.interactable = ScoreManager.Instance.GetScore() >= itemPrice;
        }
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateButtonState;
        }
    }
}
