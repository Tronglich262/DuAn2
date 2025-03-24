using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems;
        [field: SerializeField] public int Size { get; private set; } = 10;
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;


        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public int AddItem(ItemSO item, int quantity)
        {
            if (!item.IsStackable)
            {
                while (quantity > 0 && !IsInventoryFull())  // Sửa điều kiện vòng lặp
                {
                    quantity -= AddNonStackableItem(item, 1);
                }
            }
            else
            {
                quantity = AddStackableItem(item, quantity);
            }

// Chỉ gọi khi có thay đổi thực sự
            InformAboutChange();
            return quantity;
        }

        private int AddNonStackableItem(ItemSO item, int quantity)
        {
            int addedCount = 0; // Số lượng thực tế đã thêm vào túi

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = new InventoryItem { item = item, quantity = 1 };
                    addedCount++;
                    quantity--;

                    if (quantity <= 0) // Nếu đã thêm hết thì dừng vòng lặp
                        break;
                }
            }

            return addedCount; // Trả về số lượng đã thêm
        }


        private bool IsInventoryFull()
            => inventoryItems.Where(item => item.IsEmpty).Any() == false;

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                if(inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTaKe = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;
                    if (quantity > amountPossibleToTaKe)
                    {
                        inventoryItems[i] = inventoryItems[i].changeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTaKe;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i]
                            .changeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }

            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item,newQuantity);
            }

            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity
            };
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }


        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty) continue;
                returnValue[i] = inventoryItems[i];
            }

            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int itemindex1, int itemindex2)
        {
            InventoryItem item1 = inventoryItems[itemindex1];
            inventoryItems[itemindex1] = inventoryItems[itemindex2];
            inventoryItems[itemindex2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public bool IsEmpty => item == null;

        public InventoryItem changeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
            };
        }

        public static InventoryItem GetEmptyItem() => new InventoryItem
        {
            item = null,
            quantity = 0,
        };
    }
}
