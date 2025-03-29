using System.Collections;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public static event System.Action<int> EnemyDied;
    public int enemyID;
    private Animator animator;
    private bool isDead = false;
    [SerializeField] public GameObject itemDropPrefab;

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
        if (itemDropPrefab != null) // Kiểm tra nếu có vật phẩm rơi
        {
            Instantiate(itemDropPrefab, transform.position, Quaternion.identity); // Sinh vật phẩm tại vị trí của quái
        }
    
        Destroy(gameObject); // Xóa quái
        EnemyDied?.Invoke(enemyID);
    }

    
}