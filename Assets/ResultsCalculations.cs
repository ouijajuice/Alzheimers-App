using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsCalculations : MonoBehaviour
{
    // Define constants for baseline risks per age group
    private const float BaselineRiskUnder65 = 0.0005f;
    private const float BaselineRisk65To74 = 0.05f;
    private const float BaselineRisk75To84 = 0.131f;
    private const float BaselineRisk85Plus = 0.333f;

    // Age-specific weightings for cognitive and lifestyle factors
    private static readonly Dictionary<string, (float CognitiveWeight, float LifestyleWeight)> AgeWeightings = new Dictionary<string, (float, float)>
    {
        { "Under65", (0.35f, 0.65f) },
        { "65To74", (0.50f, 0.50f) },
        { "75To84", (0.65f, 0.35f) },
        { "85Plus", (0.90f, 0.10f) }
    };

    // Function to calculate the risk
    public static float CalculateRisk(int age, float cognitiveScore, float lifestyleScore)
    {
        string ageGroup = GetAgeGroup(age);
        float baselineRisk = GetBaselineRisk(ageGroup);
        (float cognitiveWeight, float lifestyleWeight) = AgeWeightings[ageGroup];

        // Apply the weights
        float weightedCognitiveRisk = cognitiveScore * cognitiveWeight;
        float weightedLifestyleRisk = lifestyleScore * lifestyleWeight;

        // Final risk calculation
        float finalRisk = baselineRisk * (weightedCognitiveRisk + weightedLifestyleRisk);
        return finalRisk * 100; // Convert to percentage
    }

    // Helper function to determine the age group
    private static string GetAgeGroup(int age)
    {
        if (age < 65) return "Under65";
        if (age >= 65 && age <= 74) return "65To74";
        if (age >= 75 && age <= 84) return "75To84";
        return "85Plus";
    }

    // Helper function to get the baseline risk based on age group
    private static float GetBaselineRisk(string ageGroup)
    {
        return ageGroup switch
        {
            "Under65" => BaselineRiskUnder65,
            "65To74" => BaselineRisk65To74,
            "75To84" => BaselineRisk75To84,
            "85Plus" => BaselineRisk85Plus,
            _ => throw new ArgumentException("Invalid age group"),
        };
    }

    // Test function to simulate inputs
    public static void Main()
    {
        int age = 50; // Example age
        float cognitiveScore = 1.5f; // Example cognitive risk factor (e.g., Test 1 score)
        float lifestyleScore = 9.352f; // Example lifestyle risk factor (e.g., aggregated scores from Tests 2, 3, 4)

        float riskPercentage = CalculateRisk(age, cognitiveScore, lifestyleScore);
        Console.WriteLine($"Alzheimer's Risk Percentage: {riskPercentage:F2}%");
    }
}
