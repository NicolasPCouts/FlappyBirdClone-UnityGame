using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadCanvas_Manager : MonoBehaviour
{
    public Text currentScore;
    public Text bestScore;

    public void SetScoreValues(int currentSc, int bestSc)
    {
        currentScore.text = currentSc.ToString();
        bestScore.text = bestSc.ToString();
    }
}
