using UnityEngine;
using UnityEngine.UI;

public class mouse : MonoBehaviour
{
    public GameObject[] otherButtons; // 5 button cần ẩn/hiện
    public Player1 playerScript; // Script Player cần bật/tắt
    private bool isActive = true; // Trạng thái hiện tại

    private void Start()
    {
        // Gán sự kiện bấm vào button
        GetComponent<Button>().onClick.AddListener(ToggleActive);
    }

    private void ToggleActive()
    {
        isActive = !isActive; // Đảo trạng thái

        // Bật/tắt 5 button khác
        foreach (GameObject btn in otherButtons)
        {
            if (btn != null)
                btn.SetActive(!isActive);
        }

        // Bật/tắt Script Player
        if (playerScript != null)
            playerScript.enabled = isActive;
    }
}
