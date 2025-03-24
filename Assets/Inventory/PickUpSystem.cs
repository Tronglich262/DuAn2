using System;
using UnityEngine;
using Inventory.Model;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null && item.Quantity > 0)
        {
            int reminder = inventoryData.AddItem(item.inventoryItem, item.Quantity);
            if (reminder == 0)
            {
                Debug.Log($"Đã nhặt hết {item.inventoryItem.name}, gọi DestroyItem().");
                item.DestroyItem();
            }
            else
            {
                Debug.Log($"Không đủ chỗ cho toàn bộ {item.inventoryItem.name}, còn lại: {reminder}");
                item.Quantity = reminder;
            }

        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
