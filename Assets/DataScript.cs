using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataScript : MonoBehaviour
{
    public double testOneScore;
    public double testTwoScore;
    public double testThreeScore;
    public double testFourScore;
    public double resultScore;
    public int age;
    public double smokingRisk;
    public double alcoholRisk;
    public double sleepRisk;
    public double exerciseRisk;
    public double familyHistoryRisk;
    public double cognitiveScore;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
