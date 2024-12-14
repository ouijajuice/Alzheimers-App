using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsCalculations : MonoBehaviour
{
    public TMP_Text percentageText; // Text field for displaying the percentage score
    public TMP_Text messageText;    // Text field for displaying the message
    public Image radialFillImage;
    public float risk;
    public int age;
    public double score;
    private DataScript data;

    private void Start()
    {
        // Fetch the DataScript component from the GameObject tagged "Data"
        data = GameObject.FindWithTag("Data").GetComponent<DataScript>();

        // Retrieve age from the data script
        age = data.age;

        // Determine risk based on age
        if (age < 65)
        {
            risk = 0.0005f;
        }
        else if (age >= 65 && age < 74)
        {
            risk = 0.05f;
        }
        else if (age >= 74 && age < 84)
        {
            risk = 0.131f;
        }
        else if (age >= 84)
        {
            risk = 0.333f;
        }

        // Calculate total score
        score = data.testOneScore + data.testTwoScore + data.testThreeScore + data.testFourScore;

        // Display the percentage score and message
        DisplayResults();
    }

    private void DisplayResults()
    {
        // Calculate percentage
        float percentage = (float)(score / 400.0 * 100);

        radialFillImage.fillAmount = percentage / 100.0f;

        // Update the percentage text
        if (percentageText != null)
        {
            percentageText.text = percentage.ToString("F2") + "%";
        }
        
    }
}
