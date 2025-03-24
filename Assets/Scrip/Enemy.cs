using System.Collections;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public static event System.Action<int> EnemyDied;
    public int enemyID;
    private Animator animator;
    private bool isDead = false; 

    public GameObject floatingTextPrefab; // Prefab của "+5"
    private ScoreManager scoreManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        scoreManager = FindObjectOfType<ScoreManager>(); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {     
        

        if (other.CompareTag("Kiem") || other.CompareTag("Khien"))
        {

            Destroy(gameObject);
            StartCoroutine(timedeley());
            scoreManager.AddScore(Random.Range(3, 8)); // Cộng điểm
            
       
        }
    }

    IEnumerator timedeley()
    {
        animator.SetBool("die", true);
        yield return new WaitForSeconds(0.3f);
    }

    

   
    public void Die()
    {
        Destroy(gameObject); // Xoá hoàn toàn quái
        EnemyDied?.Invoke(enemyID);
    }
    
}