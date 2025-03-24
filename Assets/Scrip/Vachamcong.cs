using System;
using System.Collections;
using UnityEngine;

public class Vachamcong : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    public GameObject frefab;
    public Transform spawnPoint;
    public GameObject spawnedObject; // Lưu prefab đã spawn để xóa

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
            StartCoroutine(spawnnamgiay());
        }
    }


    IEnumerator spawnnamgiay()
    {
        spawnedObject =Instantiate(frefab, spawnPoint.position, Quaternion.identity); // Spawn prefab
        yield return new WaitForSeconds(1f);
        Destroy(spawnedObject); 
    }

   
}