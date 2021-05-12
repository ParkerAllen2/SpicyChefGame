using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreCounter : MonoBehaviour
{
    public Gradient gradient;
    public float mulitplyer, bas;
    public int minTextSize, maxTextSize, textSizeMultiplyer;
    public float minDifferenceSpeed;
    public float scoreUpdateRate;

    TextMeshProUGUI scoreText;
    int score = 0;
    float averageSpeed = 0, lastSpeed = 0;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        UpdateScore();
    }

    public void AddPoints(float value)
    {
        //int points = (int)(Mathf.Pow(value, 2) * mulitplyer + bas);
        int points = (int)(value * mulitplyer + bas);
        score += points;

        averageSpeed = (averageSpeed + value) / 2;
        if (value - lastSpeed > minDifferenceSpeed)
        {
            UpdateScore(value); 
        }
        lastSpeed = value;
    }

    IEnumerator WaitUpdateScore()
    {
        yield return new WaitForSeconds(scoreUpdateRate);
        UpdateScore();
    }

    void UpdateScore()
    {
        UpdateScore(averageSpeed);
    }

    void UpdateScore(float value)
    {
        StopAllCoroutines();
        averageSpeed = 0;
        int fontSize = (int)(minTextSize + value * textSizeMultiplyer);
        fontSize = Mathf.Min(maxTextSize, fontSize);
        scoreText.fontSize = fontSize;

        scoreText.color = gradient.Evaluate(value);
        scoreText.SetText(String.Format("{0:n0}", score));
        StartCoroutine(WaitUpdateScore());
    }
}
