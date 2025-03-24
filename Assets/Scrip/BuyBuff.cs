/*using UnityEngine;
using UnityEngine.UI;

public class BuyBuff : MonoBehaviour
{
    public GameObject player; // Kéo thả Player vào đây trong Unity


    void Start()
    {
        if (player != null)
        {
            
        }
        else
        {
            Debug.LogError("Chưa gán Player vào Button Buff!");
        }

        // Gán sự kiện click cho button
        GetComponent<Button>().onClick.AddListener(OnBuffButtonClicked);
    }

    void OnBuffButtonClicked()
    {
        Debug.Log("Buff Button clicked");

        if (playerAnimator != null)
        {
           
            Debug.Log("Animator buff Triggered!");
        }
        else
        {
            Debug.LogError("Không tìm thấy Animator trên Player!");
        }
    }
}*/