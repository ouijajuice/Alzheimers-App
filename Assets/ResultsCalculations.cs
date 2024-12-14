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

    public float risk;

    public int age;
    public double score;
    DataScript data;
    private void Start()
    {
        data = GameObject.FindWithTag("Data").GetComponent<DataScript>();

        age = data.age;

        if (age < 65 )
        {
            risk = 0.0005f;
        }
        if (age >= 65 && age < 74)
        {
            risk = 0.05f;
        }
        if (age >= 74 && age < 84)
        {
            risk = 0.131f;
        }
        if (age >= 84)
        {
            risk = 0.333f;
        }
        score = data.testOneScore + data.testTwoScore + data.testThreeScore + data.testFourScore;

    }
}
