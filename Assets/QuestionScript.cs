using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScript : MonoBehaviour
{
    public GameObject nextQuestion;
    public GameObject thisQuestion;
    public Button[] answers;
    public double[] answerValues;
    public GameObject dataObj;
    private DataScript dataScript;
    private bool buttonPressed = false;

    [Header("Related data variable:")]
    public bool smoking;
    public bool alcohol;
    public bool sleep;
    public bool exercise;
    public bool familyHistory;

    private void Start()
    {
        dataObj = GameObject.FindWithTag("Data");
        dataScript = dataObj.GetComponent<DataScript>();
        for (int i = 0; i < answers.Length; i++)
        {
            int index = i; // Cache the current index to use in the lambda
            answers[i].onClick.AddListener(() => ButtonPressed(answers[index]));
        }

    }

    private void Update()
    {
        if (buttonPressed)
        {
            nextQuestion.SetActive(true);
            thisQuestion.SetActive(false);
        }
    }

    void ButtonPressed(Button answer)
    {
        int index = System.Array.IndexOf(answers, answer);
        if (smoking)
        {
            dataScript.smokingRisk = answerValues[index];
        }
        if (alcohol)
        {
            dataScript.alcoholRisk = answerValues[index];
        }
        if (sleep)
        {
            dataScript.sleepRisk = answerValues[index];
        }
        if (exercise)
        {
            dataScript.exerciseRisk = answerValues[index];
        }
        if (familyHistory)
        {
            dataScript.familyHistoryRisk = answerValues[index];
        }
        buttonPressed = true;
    }
}
