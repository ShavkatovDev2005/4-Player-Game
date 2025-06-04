using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class savollar : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text questionText; // Soruyu göstereceğiz
    public Button[] answerButtons; // Cevaplar için butonlar (4 buton)
    public TMP_Text scoreText; // Puanı göstermek için

    private int currentQuestionIndex = 0; // Geçerli soru indeksi
    public int correctScore = 0; // Doğru puan
    public int wrongScore = 0; // Yanlış puan

    private List<Question> questions = new List<Question>(); // Soruları tutacak liste

    void Start()
    {
        // Soruları oluştur
        questions.Add(new Question("Hangi dil Unity icin en yaygın kullanılan dilidir?", new string[] { "Java", "C#", "C++", "Python" }, 1));
        questions.Add(new Question("Hangi sirket Unity oyun motorunu gelistirmistir?", new string[] { "Epic Games", "Unity Technologies", "Blizzard", "Valve" }, 1));
        questions.Add(new Question("Unity ucretsiz mi?", new string[] { "Evet", "Hayır", "Sadece profesyoneller icin", "Sadece eğitmenler icin" }, 0));
        questions.Add(new Question("Unity hangi yıl piyasaya surulmuştur?", new string[] { "2000", "2005", "2010", "2015" }, 1));
        questions.Add(new Question("Hangi platformlar Unity ile uyumludur?", new string[] { "PC", "Mac", "Mobil", "Hepsi" }, 3));
        questions.Add(new Question("Unity hangi programlama dilini kullanarak script yazılır?", new string[] { "JavaScript", "Python", "C#", "Ruby" }, 2));
        questions.Add(new Question("Unity'de 3D modelleme yapmak icin hangi program kullanilir?", new string[] { "Blender", "Photoshop", "Unity Editor", "Maya" }, 0));
        questions.Add(new Question("Hangi grafik API'si Unity ile uyumludur?", new string[] { "OpenGL", "Vulkan", "DirectX", "Hepsi" }, 3));
        questions.Add(new Question("Unity'nin en son surumu nedir?", new string[] { "2022", "2023", "2024", "2021" }, 2));
        questions.Add(new Question("Unity'nin ucretsiz surumu ne adla bilinir?", new string[] { "Unity Pro", "Unity Plus", "Unity Free", "Unity Personal" }, 3));

        // İlk soruyu yükle
        LoadQuestion();

        // Butonlara tıklama olaylarını ekleyin
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i; // local copy to avoid closure issue
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }
    }

    // Soruyu yükle
    void LoadQuestion()
    {
        // Soruları rastgele karıştır
        int randomIndex = Random.Range(0, questions.Count);
        Question currentQuestion = questions[randomIndex];

        // Soruyu ve cevapları UI'ya yaz
        questionText.text = currentQuestion.question;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[i];
        }

        currentQuestionIndex = randomIndex;
    }

    // Cevap seçildiğinde
    void OnAnswerSelected(int index)
    {
        Question currentQuestion = questions[currentQuestionIndex];

        if (index == currentQuestion.correctAnswerIndex)
        {
            correctScore++;
        }
        else
        {
            wrongScore++;
        }

        // Skoru güncelle
        scoreText.text = "To'g'ri: " + correctScore + "   Xato: " + wrongScore;

        // Yeni soru yükle
        LoadQuestion();
    }
    public void quit()
    {
        SceneManager.LoadScene(0);
    }
}


[System.Serializable]
public class Question
{
    public string question; // Soru metni
    public string[] answers; // Cevaplar
    public int correctAnswerIndex; // Doğru cevabın index'i

    public Question(string question, string[] answers, int correctAnswerIndex)
    {
        this.question = question;
        this.answers = answers;
        this.correctAnswerIndex = correctAnswerIndex;
    }
}
