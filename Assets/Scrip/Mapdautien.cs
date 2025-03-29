using Inventory.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Mapdautien : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //them
            inventoryData.SaveInventory();
            SceneManager.LoadScene("MapDau");
        }
    }
}