using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class QuizManager : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_Text questionText; // TextMeshPro for displaying the question
    public Button[] answerButtons; // Buttons for answer options
    public TMP_Text scoreText; // TextMeshPro for displaying the score
    public GameObject endOfTestButton;

    [Header("Quiz Data")]
    public Question[] questions; // Array of questions

    [Header("Saved Data")]
    public GameObject dataObj;
    public int testNum;

    private int currentQuestionIndex = 0; // Index of the current question
    private double totalScore = 0; // Total score based on answer values

    private void Start()
    {
        dataObj = GameObject.FindWithTag("Data");
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
        UpdateScore();
        DisplayQuestion();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + totalScore;
        dataObj.GetComponent<DataScript>().cognitiveScore = totalScore;
    }

    private void EndQuiz()
    {
        questionText.text = "Test complete. Please return to the dashboard.";
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
        endOfTestButton.SetActive(true);
        dataObj.GetComponent<DataScript>().cognitiveScore = totalScore;
    }
}

[System.Serializable]
public class Question
{
    public string question; // The question text
    public string[] answers; // The answer options
    public double[] answerValues; // The values assigned to each answer
}
