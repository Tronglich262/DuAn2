using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using UnityEditor;

namespace Inventory.UI
{

    public class UiinventoryPage : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] private UiinventoryItem itemPrefab;
        [SerializeField] private RectTransform contentpanel;
        [SerializeField] private UIInventoryDescription itemdescription;
        [SerializeField] private MouseFollower mouseFollower;
        List<UiinventoryItem> ListOfUiItem = new List<UiinventoryItem>();
        public int curentlyDraggendItemIndex = -1;

        public event Action<int> OnDescriptionRequaested,
            OnItemActionRequested,
            OnstartDragging;

        public event Action<int, int> OnSwapItems;

        private void Awake()
        {
            hide();
            mouseFollower.Toggle(false);
            itemdescription.ResetDescription();
        }

        public void InitializeInventoryUI(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UiinventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentpanel);
                ListOfUiItem.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        public void UpdateDescription(int itemIndex, Sprite itemItemImage, string itemName, string itemDescription)
        {
            itemdescription.SetDescription(itemItemImage, itemName, itemDescription);
            DeselectAllItems();
            ListOfUiItem[itemIndex].Deselect();
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (ListOfUiItem.Count > itemIndex)
            {
                ListOfUiItem[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleShowItemActions(UiinventoryItem inventoryItemUI)
        {

        }

        private void HandleEndDrag(UiinventoryItem inventoryItemUI)
        {
            ResetDraggtedItem();
        }

        private void HandleSwap(UiinventoryItem inventoryItemUI)
        {
            int index = ListOfUiItem.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }

            OnSwapItems?.Invoke(curentlyDraggendItemIndex, index);
            HandleItemSelection(inventoryItemUI);
        }

        private void ResetDraggtedItem()
        {
            mouseFollower.Toggle(false);
            curentlyDraggendItemIndex = -1;
        }


        private void HandleBeginDrag(UiinventoryItem inventoryItemUI)
        {
            int index = ListOfUiItem.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            curentlyDraggendItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnstartDragging?.Invoke(index);

        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UiinventoryItem inventoryItemUI)
        {
            int index = ListOfUiItem.IndexOf(inventoryItemUI);
            if (index == -1) return;
            OnDescriptionRequaested?.Invoke(index);
        }

        public void show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection()
        {
            itemdescription.ResetDescription();
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (UiinventoryItem item in ListOfUiItem)
            {
                item.Deselect();
            }
        }

        public void hide()
        {
            gameObject.SetActive(false);
            ResetDraggtedItem();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void ResetAllItems()
        {
            foreach (var item in ListOfUiItem)
            {
                item.ResetData();
                item.Deselect();
                
            }
        }
    }
}
