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
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
