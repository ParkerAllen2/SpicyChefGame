using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public Gradient gradient;
    public float mulitplyer, bas;
    public int minTextSize, maxTextSize, textSizeMultiplyer;

    TextMeshProUGUI scoreText;
    int score = 0;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void AddPoints(float value)
    {
        int points = (int)(Mathf.Pow(value, 2) * mulitplyer + bas);
        score += points;
        scoreText.SetText(score + "");

        scoreText.color = gradient.Evaluate(value);

        int fontSize = (int)(minTextSize + value * textSizeMultiplyer);
        fontSize = Mathf.Min(maxTextSize, fontSize);
        scoreText.fontSize = fontSize;
    }
}
