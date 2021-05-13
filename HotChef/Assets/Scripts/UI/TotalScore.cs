using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TotalScore : MonoBehaviour
{

    TextMeshProUGUI scoreText;
    int score = 0;
    public float baseMultiplier;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        UpdateScore(0,0);
    }

    public void UpdateScore(float velocity, float combo)
    {
        score += (int)(velocity * (baseMultiplier + combo));
        scoreText.SetText(String.Format("{0:n0}", score));
    }
}
