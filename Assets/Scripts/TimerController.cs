using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText; // UI'de zamanı gösteren metin
    public TextMeshProUGUI scoreText; // UI'de skoru gösteren metin
    private float timer = 0f; // Zamanlayıcı değeri
    private bool gameStarted = false; // Oyun başladı mı?

    private BallController ballController; // Topun kontrolünü sağlayan bileşen

    void Start()
    {
        // Oyun başladığında zamanlayıcıyı başlat
        StartTimer();

        // BallController bileşenini bul
        ballController = FindObjectOfType<BallController>();

        // Skor textini başlangıçta gizle
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Oyun başladıysa zamanlayıcıyı güncelle
        if (gameStarted)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    // Zamanlayıcıyı başlatan fonksiyon
    public void StartTimer()
    {
        gameStarted = true;
        timer = 0f;
        UpdateTimerDisplay();
    }

    // Zamanlayıcıyı durduran fonksiyon
    public void StopTimer()
    {
        gameStarted = false;
    }

    // Zamanlayıcıyı sıfırlayan fonksiyon
    public void ResetTimer()
    {
        timer = 0f;
        UpdateTimerDisplay();
    }

    // Zamanlayıcı değerini döndüren fonksiyon
    public float GetTimer()
    {
        return timer;
    }

    // UI üzerinde zamanı güncelleyen fonksiyon
    void UpdateTimerDisplay()
    {
        timerText.text = "Süre: " + timer.ToString("F1"); // Zamanı UI üzerinde göster
    }

    // Doğru cevap alındığında çağrılan fonksiyon
    public void DecreaseTime()
    {
        timer = Mathf.Max(0f, timer - 1f); // 1 saniye azalt
        UpdateTimerDisplay();
    }

    // Yanlış cevap alındığında çağrılan fonksiyon
    public void IncreaseTime()
    {
        timer += 2f; // 2 saniye artır
        UpdateTimerDisplay();
    }

    // Oyun bittiğinde çağrılan fonksiyon
    public void FinishGame()
    {
        // Zamanlayıcıyı durdur
        StopTimer();
        // Gerekirse ekstra işlemler yapılabilir

        // Skoru Canvas üzerinde göster
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true); // Text'i aktifleştir
            scoreText.text = "Oyun Bitti! Skorunuz: " + timer.ToString("F1") + " saniye";

            // BallController'ı devre dışı bırak
            if (ballController != null)
            {
                ballController.DisableControls();
            }
        }

        Debug.Log("Oyun bitti! Toplam süre: " + timer.ToString("F1") + " saniye");
    }

    // Bitiş noktasına ulaşıldığında çağrılan fonksiyon
    public void ReachedFinishPoint()
    {
        FinishGame();
    }

    // ScoreText bileşenini ayarlamak için yardımcı fonksiyon
    public void SetScoreText(TextMeshProUGUI text)
    {
        scoreText = text;
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false); // Başlangıçta text'i gizle
        }
    }
}
