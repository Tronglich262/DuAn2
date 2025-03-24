using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMot : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 🔥 Lưu vị trí nhân vật trước khi đổi map
            PlayerPrefs.SetFloat("PlayerPosX", -10.42f);
            PlayerPrefs.SetFloat("PlayerPosY", 3.26f);
            PlayerPrefs.SetFloat("PlayerPosZ", -0.004f);
            PlayerPrefs.Save();

            SceneManager.LoadScene("MapDau"); // Chuyển về map đầu
        }
    }
}