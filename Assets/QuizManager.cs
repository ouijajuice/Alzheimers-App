using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class QuizManager : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_Text questionText; // TextMeshPro for displaying the question
    public Button[] answerButtons; // Buttons for answer options
    public TMP_Text scoreText; // TextMeshPro for displaying the score

    [Header("Quiz Data")]
    public Question[] questions; // Array of questions

    private int currentQuestionIndex = 0; // Index of the current question
    private int totalScore = 0; // Total score based on answer values

    private void Start()
    {
        DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            Question currentQuestion = questions[currentQuestionIndex];

            // Set the question text
            questionText.text = currentQuestion.question;

            // Set the answers on the buttons
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i < currentQuestion.answers.Length)
                {
                    answerButtons[i].gameObject.SetActive(true);
                    answerButtons[i].GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[i];

                    // Assign the button click event
                    int answerIndex = i; // Local copy to avoid closure issues
                    answerButtons[i].onClick.RemoveAllListeners();
                    answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex));
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            EndQuiz();
        }
    }

    private void CheckAnswer(int answerIndex)
    {
        // Add the value of the selected answer to the total score
        totalScore += questions[currentQuestionIndex].answerValues[answerIndex];

        currentQuestionIndex++;
        UpdateScoreText();
        DisplayQuestion();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + totalScore;
    }

    private void EndQuiz()
    {
        questionText.text = "Quiz Complete! Your total score: " + totalScore;
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}

[System.Serializable]
public class Question
{
    public string question; // The question text
    public string[] answers; // The answer options
    public int[] answerValues; // The values assigned to each answer
}
