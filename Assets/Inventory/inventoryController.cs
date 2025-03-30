using System.Collections.Generic;
using System.Text;
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
        [SerializeField] private AudioClip dropClip;
        [SerializeField] private AudioSource audioSource;
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

            if (infoPanel != null)
            {
                infoPanel.SetActive(false);
            }

            // 🔹 Load trạng thái mở inventory từ PlayerPrefs
            if (PlayerPrefs.GetInt("InventoryOpen", 0) == 1)
            {
                invontoryUI.show();
                foreach (var item in inventoryDaTa.GetCurrentInventoryState())
                {
                    invontoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
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
            invontoryUI.ResetAllItems(); // Xóa toàn bộ UI trước khi cập nhật
            foreach (var item in inventoryState)
            {
                if (!item.Value.IsEmpty) // 🔹 Chỉ cập nhật nếu ô có item
                {
                    invontoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
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

        private void HandleItemActionRequest(int itemIndex)
        {
          InventoryItem inventoryItem = inventoryDaTa.GetItemAt(itemIndex);
          if (inventoryItem.IsEmpty)
              return;
          IItemAction itemAction = inventoryItem.item as IItemAction;
          if (itemAction != null)
          {
              invontoryUI.ShowItemActions(itemIndex);
              invontoryUI.AddAction(itemAction.ActionName, () => PerforAction(itemIndex));
          }
          IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
          if (destroyableItem != null)
          {
              invontoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
          }
        }

        public void DropItem(int itemIndex, int quantity)
        {
            inventoryDaTa.RemoveItem(itemIndex, quantity);
            invontoryUI.ResetSelection();
            audioSource.PlayOneShot(dropClip);
        }

        public void PerforAction(int itemIndex)
        {
           InventoryItem inventoryItem = inventoryDaTa.GetItemAt(itemIndex);
           if (inventoryItem.IsEmpty) return;
           IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
           if (destroyableItem != null)
           {
               inventoryDaTa.RemoveItem(itemIndex, 1);
           }
           IItemAction itemAction = inventoryItem.item as IItemAction;
           if (itemAction != null)
           {
               itemAction.PerforAction(gameObject, inventoryItem.itemState);
           }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryDaTa.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty) return;

            invontoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }
        

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryDaTa.SwapItems(itemIndex_1, itemIndex_2);
            invontoryUI.ResetAllItems(); // 🔹 Xóa UI item đang kéo
            UpdateInventoryUI(inventoryDaTa.GetCurrentInventoryState());
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
            string description = PrepareDescription(inventoryItem);
            invontoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName}" + $": {inventoryItem.itemState[i].value} / {inventoryItem.item.DefaultParametersList[i].value}");
               sb.AppendLine();
            }
            return sb.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                bool isOpen = invontoryUI.isActiveAndEnabled;
        
                if (!isOpen)
                {
                    invontoryUI.show();
                    foreach (var item in inventoryDaTa.GetCurrentInventoryState())
                    {
                        invontoryUI.UpdateData(item.Key,
                            item.Value.item.ItemImage,
                            item.Value.quantity);
                    }
                    PlayerPrefs.SetInt("InventoryOpen", 1); // 🔹 Lưu trạng thái mở inventory
                }
                else
                {
                    invontoryUI.hide();
                    PlayerPrefs.SetInt("InventoryOpen", 0); // 🔹 Lưu trạng thái đóng inventory
                }

                PlayerPrefs.Save(); // Lưu dữ liệu lại
            }
        }
    }
}
