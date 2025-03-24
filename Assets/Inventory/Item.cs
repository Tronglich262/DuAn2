using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO inventoryItem { get; private set; }

    [field: SerializeField] public int Quantity { get; set; } = 1;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float duration = 0.3f;

    private SpriteRenderer spriteRenderer;
    private Collider2D itemCollider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<Collider2D>();

        if (spriteRenderer != null && inventoryItem != null)
        {
            spriteRenderer.sprite = inventoryItem.ItemImage;
        }
    }

    internal void DestroyItem()
    {
        if (itemCollider != null)
        {
            itemCollider.enabled = false;
        }

        StartCoroutine(AnimateItemPickup());
    }

    private IEnumerator AnimateItemPickup()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }

        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }

        Destroy(gameObject);
    }
}