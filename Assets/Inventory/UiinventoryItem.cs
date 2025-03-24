using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

namespace Inventory.UI
{


    public class UiinventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler,
        IEndDragHandler, IDropHandler, IDragHandler
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text quantityTxt;
        [SerializeField] private Image borderImage;

        public event Action<UiinventoryItem> OnItemClicked,
            OnItemDroppedOn,
            OnItemBeginDrag,
            OnItemEndDrag,
            OnRightMouseBtnClick;

        private bool empty = true;

        public void Awake()
        {
            ResetData();
            Deselect();
        }

        public void ResetData()
        {
            this.itemImage.gameObject.SetActive(false);
            empty = true;
        }

        public void OnDestroy()
        {
            borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            this.quantityTxt.text = quantity + "";
            empty = false;
        }

        public void Deselect()
        {
            borderImage.enabled = true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty) return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn.Invoke(this);

        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }



        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
