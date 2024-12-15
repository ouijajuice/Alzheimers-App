using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsCalculations : MonoBehaviour
{
    public TMP_Text percentageText;  // Text field for displaying the percentage score
    public TMP_Text messageText;     // Text field for displaying the message
    public Image radialFillImage;    // Image to display the radial fill
    public float risk;               // Final calculated Alzheimer's risk
    public double age;                  // Age of the user
    public double cognitiveScore;    // Cognitive test score
    public double lifestyleScore;    // Lifestyle-related score
    private DataScript data;

    private void Start()
    {
        // Fetch the DataScript component from the GameObject tagged "Data"
        data = GameObject.FindWithTag("Data").GetComponent<DataScript>();

        // Retrieve age from the data script
        age = data.age;

        // Retrieve cognitive and lifestyle scores
        cognitiveScore = data.cognitiveScore; // Value derived from cognitive tests
        lifestyleScore = CalculateLifestyleScore();

        // Determine and calculate risk
        CalculateRisk();

        // Display results
        DisplayResults();
    }

    private double CalculateLifestyleScore()
    {
        // Retrieve lifestyle factors from the data script
        double smokingRisk = data.smokingRisk;       // Smoking risk multiplier
        double alcoholRisk = data.alcoholRisk;       // Alcohol consumption risk multiplier
        double sleepRisk = data.sleepRisk;           // Sleep pattern risk multiplier
        double exerciseRisk = data.exerciseRisk;     // Physical activity risk multiplier
        double familyHistoryRisk = data.familyHistoryRisk; // Family history risk multiplier

        // Multiply all lifestyle-related risk factors
        return smokingRisk * alcoholRisk * sleepRisk * exerciseRisk * familyHistoryRisk;
    }

    private void CalculateRisk()
    {
        // Age-specific baseline risk and weight distribution
        float baselineRisk;
        float cognitiveWeight;
        float lifestyleWeight;

        if (age < 65)
        {
            baselineRisk = 0.0005f;
            cognitiveWeight = 0.35f;
            lifestyleWeight = 0.65f;
        }
        else if (age >= 65 && age < 74)
        {
            baselineRisk = 0.05f;
            cognitiveWeight = 0.5f;
            lifestyleWeight = 0.5f;
        }
        else if (age >= 74 && age < 84)
        {
            baselineRisk = 0.131f;
            cognitiveWeight = 0.65f;
            lifestyleWeight = 0.35f;
        }
        else // Age >= 85
        {
            baselineRisk = 0.333f;
            cognitiveWeight = 0.9f;
            lifestyleWeight = 0.1f;
        }

        // Weighted sum of cognitive and lifestyle scores
        double weightedRisk = (cognitiveScore * cognitiveWeight) + (lifestyleScore * lifestyleWeight);

        // Final Alzheimer's risk calculation
        risk = (float)(baselineRisk * weightedRisk);
    }

    private void DisplayResults()
    {
        // Convert risk to percentage
        float riskPercentage = risk * 100;

        // Update radial fill
        if (radialFillImage != null)
        {
            radialFillImage.fillAmount = riskPercentage / 100.0f;
        }

        // Update the percentage text
        if (percentageText != null)
        {
            percentageText.text = "Risk: " + riskPercentage.ToString("F2") + "%";
        }

        // Update the message text
        if (messageText != null)
        {
            if (riskPercentage < 25)
            {
                messageText.text = "Low risk. Keep up the good work!";
            }
            else if (riskPercentage < 50)
            {
                messageText.text = "Moderate risk. Consider lifestyle adjustments.";
            }
            else
            {
                messageText.text = "High risk. Take action to reduce your risk.";
            }
        }
    }
}
