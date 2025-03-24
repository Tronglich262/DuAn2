using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Inventory.UI
{


    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;

        public void Awake()
        {
            ResetDescription();
        }

        public void ResetDescription()
        {
            this.itemImage.gameObject.SetActive(false);
            this.title.text = "";
            this.description.text = "";
        }

        public void SetDescription(Sprite sprite, string itemName, String itemDescription)
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            this.title.text = itemName;
            this.description.text = itemDescription;
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
}
