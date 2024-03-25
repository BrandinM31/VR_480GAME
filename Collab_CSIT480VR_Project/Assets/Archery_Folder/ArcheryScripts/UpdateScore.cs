using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{
    public TextMeshProUGUI scoreTxt;

    private int score;
    private void Start()
    {
        score = 0;
        SetScoreTxt();
    }

    public void AddScore(int points)
    {
        score += points;
        SetScoreTxt();
    }

    private void SetScoreTxt()
    {
        scoreTxt.text = "Score: " + score.ToString();
    }
}
