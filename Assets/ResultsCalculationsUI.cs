using UnityEngine;
using UnityEngine.UI;

public class ResultsCalculatorUI : MonoBehaviour
{
    // UI Elements to be assigned in the Inspector
    [Header("Input Fields")]
    public InputField ageInputField; // User's age
    public InputField cognitiveScoreInputField; // Cognitive test score
    public InputField lifestyleScoreInputField; // Lifestyle test score (aggregated)

    [Header("Output")]
    public Text riskPercentageText; // Displays the risk percentage

    // Age-specific weightings for cognitive and lifestyle factors
    private static readonly (float CognitiveWeight, float LifestyleWeight)[] AgeWeightings =
    {
        (0.35f, 0.65f), // Under 65
        (0.50f, 0.50f), // Age 65-74
        (0.65f, 0.35f), // Age 75-84
        (0.90f, 0.10f)  // Age 85+
    };

    // Baseline risks
    private static readonly float[] BaselineRisks = { 0.0005f, 0.05f, 0.131f, 0.333f };

    public void CalculateRisk()
    {
        // Validate inputs
        if (!int.TryParse(ageInputField.text, out int age) ||
            !float.TryParse(cognitiveScoreInputField.text, out float cognitiveScore) ||
            !float.TryParse(lifestyleScoreInputField.text, out float lifestyleScore))
        {
            riskPercentageText.text = "Invalid input. Please check your entries.";
            return;
        }

        // Get age group index and baseline risk
        int ageGroupIndex = GetAgeGroupIndex(age);
        if (ageGroupIndex == -1)
        {
            riskPercentageText.text = "Age must be 0 or higher.";
            return;
        }

        float baselineRisk = BaselineRisks[ageGroupIndex];
        (float cognitiveWeight, float lifestyleWeight) = AgeWeightings[ageGroupIndex];

        // Calculate risk
        float weightedCognitiveRisk = cognitiveScore * cognitiveWeight;
        float weightedLifestyleRisk = lifestyleScore * lifestyleWeight;
        float finalRisk = baselineRisk * (weightedCognitiveRisk + weightedLifestyleRisk);

        // Display result
        riskPercentageText.text = $"Alzheimer's Risk: {finalRisk * 100:F2}%";
    }

    private int GetAgeGroupIndex(int age)
    {
        if (age < 65) return 0;
        if (age >= 65 && age <= 74) return 1;
        if (age >= 75 && age <= 84) return 2;
        if (age >= 85) return 3;
        return -1; // Invalid age
    }
}

