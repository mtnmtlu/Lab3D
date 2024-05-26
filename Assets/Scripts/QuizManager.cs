using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance; // Tekil bir QuizManager örneği için statik referans

    public GameObject quizPanel; // Quiz paneli
    public TMP_Text questionText; // Soru metni
    public TMP_InputField answerInput; // Kullanıcı cevap girişi
    public Button submitButton; // Cevabı gönderme butonu

    private string currentAnswer; // Şu anki doğru cevap
    private bool showingQuestion; // Şu an soru gösteriliyor mu?

    private TimerController timerController; // Zamanlayıcıyı kontrol eden referans

    private void Awake()
    {
        // QuizManager singleton yapısını oluştur
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Quiz panelini gizle, soru gösterilmiyor olarak ayarla
        quizPanel.SetActive(false);
        showingQuestion = false;

        // Zamanlayıcı kontrolcüsünü bul
        timerController = FindObjectOfType<TimerController>();
    }

    // Quiz panelinde soruyu göstermek için çağrılan fonksiyon
    public void ShowQuestion(string question, string correctAnswer)
    {
        // Quiz panelini aktifleştir
        quizPanel.SetActive(true);

        // Soru metnini güncelle
        questionText.text = question;

        // Doğru cevabı güncelle
        currentAnswer = correctAnswer;

        // Soru gösteriliyor olarak ayarla
        showingQuestion = true;

        // AnswerInput alanını otomatik olarak seç
        StartCoroutine(ActivateInputField());
    }

    // Coroutine ile AnswerInput alanını seçilmiş hale getir
    private System.Collections.IEnumerator ActivateInputField()
    {
        // Wait for end of frame before selecting the input field
        yield return new WaitForEndOfFrame();

        // Activate and select the input field
        answerInput.ActivateInputField();
    }

    // Rastgele 4 işlem sorusu oluştur
    public void GenerateRandomMathQuestion()
    {
        int num1, num2, answer;
        string question;

        int operatorIndex = Random.Range(0, 4);  // 0 ile 3 arasında rastgele bir sayı

        switch (operatorIndex)
        {
            case 0:
                num1 = Random.Range(1, 51);  // 1 ile 50 arasında rastgele bir sayı
                num2 = Random.Range(1, 51);  // 1 ile 50 arasında rastgele bir sayı
                question = $"{num1} + {num2} = ?";
                answer = num1 + num2;
                break;
            case 1:
                num1 = Random.Range(11, 51);  // 11 ile 50 arasında rastgele bir sayı
                num2 = Random.Range(1, num1);  // 1 ile num1 arasında rastgele bir sayı
                question = $"{num1} - {num2} = ?";
                answer = num1 - num2;
                break;
            case 2:
                num1 = Random.Range(1, 21);  // 1 ile 20 arasında rastgele bir sayı
                num2 = Random.Range(1, 21);  // 1 ile 20 arasında rastgele bir sayı
                question = $"{num1} * {num2} = ?";
                answer = num1 * num2;
                break;
            case 3:
                // Bölme işleminde bölünen bölücüden büyük olmalıdır ve tam bölünebilmelidir
                num2 = Random.Range(1, 21);  // 1 ile 20 arasında rastgele bir sayı
                num1 = num2 * Random.Range(1, 21);  // num2'nin katları arasında rastgele bir sayı
                question = $"{num1} / {num2} = ?";
                answer = num1 / num2;
                break;
            default:
                question = "Hatalı işlem";
                answer = 0;
                break;
        }

        ShowQuestion(question, answer.ToString());
    }

    // Kullanıcının cevabı gönderdiği zaman çağrılan fonksiyon
    public void SubmitAnswer()
    {
        int userAnswer;
        bool isNumeric = int.TryParse(answerInput.text.Trim(), out userAnswer);

        if (!isNumeric)
        {
            Debug.Log("Geçersiz cevap! Lütfen bir sayı girin.");
            return;
        }

        // Kullanıcının girdiği cevap ile doğru cevabı karşılaştır
        bool isCorrect = userAnswer == int.Parse(currentAnswer);

        if (isCorrect)
        {
            Debug.Log("Doğru!");
            // Zamanlayıcıya 1 saniye azaltma yaptır
            timerController.DecreaseTime();
        }
        else
        {
            Debug.Log("Yanlış!");
            // Zamanlayıcıya 2 saniye artırma yaptır
            timerController.IncreaseTime();
        }

        // Quiz panelini gizle, cevap giriş alanını temizle
        quizPanel.SetActive(false);
        answerInput.text = string.Empty;

        // Soru gösterimi bitti
        showingQuestion = false;

        // Topun kontrolünü etkinleştir
        BallController ballController = FindObjectOfType<BallController>();
        if (ballController != null)
        {
            ballController.EnableControls();
        }
    }

    // Şu an soru gösteriliyor mu?
    public bool IsShowingQuestion()
    {
        return showingQuestion;
    }

    private void Update()
    {
        // Eğer soru gösteriliyorsa ve kullanıcı Enter tuşuna bastıysa cevabı gönder
        if (showingQuestion && Input.GetKeyDown(KeyCode.Return))
        {
            SubmitAnswer();
        }
    }
}
