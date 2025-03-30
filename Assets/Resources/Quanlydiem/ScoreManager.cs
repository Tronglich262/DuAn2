using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    private int currentScore;

    public event Action OnScoreChanged; // Sự kiện khi điểm thay đổi

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ điểm khi đổi Scene
            LoadScore(); // Đọc điểm từ PlayerPrefs khi game khởi động
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI(); // Hiển thị điểm ngay lập tức khi vào game
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        SaveScore();
    }

    public bool SpendScore(int cost)
    {
        if (currentScore >= cost)
        {
            currentScore -= cost;
            SaveScore();
            return true;
        }
        return false;
    }

    public void ResetScore() // Thêm chức năng Reset điểm
    {
        currentScore = 0;
        SaveScore();
    }

    void SaveScore()
    {
        PlayerPrefs.SetInt("SavedScore", currentScore); // Lưu điểm vào bộ nhớ
        PlayerPrefs.Save();
        UpdateScoreUI();
        OnScoreChanged?.Invoke(); // Gọi sự kiện cập nhật UI
    }

    void LoadScore()
    {
        currentScore = PlayerPrefs.GetInt("SavedScore", 0); // Đọc điểm đã lưu
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText == null)
        {
            scoreText = FindObjectOfType<TextMeshProUGUI>(); // Tìm lại UI khi load Scene mới
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore; // Cập nhật UI ngay lập tức
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    void OnEnable()
    {
        LoadScore(); // Đảm bảo điểm hiển thị ngay khi Scene mới tải
    }
}