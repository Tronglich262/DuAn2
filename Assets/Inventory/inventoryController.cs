using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;
using Inventory.Model;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UiinventoryPage invontoryUI;

        [SerializeField] private InventorySO inventoryDaTa;
        public List<InventoryItem> initialItems = new List<InventoryItem>();
        public GameObject infoPanel; // Bảng thông tin cần hiện
        public Button openPanelButton; // Button để mở bảng
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private void Start()
        {
            PrepareUI();
            PrepareInventoryDaTa();
            if (openPanelButton != null)
            {
                openPanelButton.onClick.AddListener(TogglePanel);
            }

            // Ẩn bảng lúc đầu
            if (infoPanel != null)
            {
                infoPanel.SetActive(false);
            }
        }
        

        void TogglePanel()
        {
            if (infoPanel != null)
            {
                bool isActive = infoPanel.activeSelf;
                infoPanel.SetActive(!isActive);

                // Tắt/bật tương tác để không chặn button khác
                CanvasGroup canvasGroup = infoPanel.GetComponent<CanvasGroup>();
                if (canvasGroup != null)
                {
                    canvasGroup.blocksRaycasts = !isActive;
                }
            }
        }

        private void PrepareInventoryDaTa()
        {
            inventoryDaTa.Initialize();
           inventoryDaTa.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if(item.IsEmpty) continue;
                inventoryDaTa.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            invontoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                invontoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        public void PrepareUI()
        {
            invontoryUI.InitializeInventoryUI(inventoryDaTa.Size);
            this.invontoryUI.OnDescriptionRequaested += HandleDescriptionRequest;
            this.invontoryUI.OnSwapItems += HandleSwapItems;
            this.invontoryUI.OnstartDragging += HandleDragging;
            this.invontoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int obj)
        {
            throw new System.NotImplementedException();
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryDaTa.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty) return;
            invontoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }
        

        private void HandleSwapItems(int itemindex_1, int itemIndex_2)
        {
            inventoryDaTa.SwapItems(itemindex_1, itemIndex_2);

        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryDaTa.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                invontoryUI.ResetSelection();
                return;
            }

            ItemSO item = inventoryItem.item;
            invontoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (invontoryUI.isActiveAndEnabled == false)
                {
                    invontoryUI.show();
                    foreach (var item in inventoryDaTa.GetCurrentInventoryState())
                    {
                        invontoryUI.UpdateData(item.Key,
                            item.Value.item.ItemImage,
                            item.Value.quantity);
                    }
                }
                else
                {
                    invontoryUI.hide();
                }
            }
        }
    }
}
