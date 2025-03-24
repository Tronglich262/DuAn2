using System.Collections;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject box1; // Box 1
    public GameObject box2; // Box 2

    void Start()
    {
        StartCoroutine(BoxLoop());
    }

    IEnumerator BoxLoop()
    {
        while (true) // Lặp vô hạn
        {
            // Bật box1, chờ 5s rồi tắt
            box1.SetActive(true);
            yield return new WaitForSeconds(5f);
            box1.SetActive(false);

            // Bật box2, chờ 5s rồi tắt
            box2.SetActive(true);
            yield return new WaitForSeconds(5f);
            box2.SetActive(false);
        }
    }
}