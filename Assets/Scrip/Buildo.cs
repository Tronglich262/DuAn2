using UnityEngine;
using UnityEngine.UI; // Import UI để dùng Button

public class Buildo : MonoBehaviour
{
    public GameObject builddo; // Đối tượng cần hiện
    public Button openButton;  // Button để mở bảng

    private bool isOpen = false; // Kiểm tra trạng thái

    void Start()
    {
        if (builddo != null)
        {
            builddo.SetActive(false); // Ẩn lúc đầu
        }

        if (openButton != null)
        {
            openButton.onClick.AddListener(ToggleBuilddo);
        }
    }

    void ToggleBuilddo()
    {
        isOpen = !isOpen;
        builddo.SetActive(isOpen);
    }
}