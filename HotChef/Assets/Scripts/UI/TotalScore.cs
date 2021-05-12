using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TotalScore : MonoBehaviour
{

    TextMeshProUGUI scoreText;
    int score = 0;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        UpdateScore(0);
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.SetText(String.Format("{0:n0}", score));
    }
}
