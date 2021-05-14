using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlord : MonoBehaviour
{
    public BallController ball;
    public SpeedBar odometer;
    public TotalScore totalScore;
    public ComboScore comboScore;
    public ScreenBorder border;
    public LightController lightController;

    public float highSpeed, lowSpeed;
    public float max, min;
    float normalizedVelocity;

    public float scoreUpdateRate;
    float nextScoreUpdate = 0;

    public float comboUpdateRate;
    float nextComboUpdate = 0;

    bool playing;
    bool isHighSpeed, isLowSpeed;

    private void Start()
    {
        Time.timeScale = 1;

    }

    private void Update()
    {
        if (GameController.Instance.gameOver) return;

        normalizedVelocity = (normalizedVelocity + Mathf.Max(0, (ball.GetSpeed() - min) / max)) / 2;
        isHighSpeed = normalizedVelocity >= highSpeed;
        isLowSpeed = normalizedVelocity < lowSpeed;

        CheckGameState();

        ball.UpdateBall(isHighSpeed, isLowSpeed, normalizedVelocity);
        border.UpdateWalls(isHighSpeed, isLowSpeed);
        odometer.UpdateBar(normalizedVelocity);
        lightController.UpdateGlobalLight(normalizedVelocity);

        
        if (nextScoreUpdate < Time.time)
        {
            float combo = comboScore.CheckHighSpeed(isHighSpeed);
            totalScore.UpdateScore(normalizedVelocity, combo);
            nextScoreUpdate = Time.time + scoreUpdateRate;
        }

        if (nextComboUpdate < Time.time)
        {
            comboScore.UpdateCombo(isHighSpeed);
            nextComboUpdate = Time.time + comboUpdateRate;
        }
    }

    public void CheckGameState()
    {
        if (!playing)
        {
            playing = isHighSpeed;
            return;
        }
        if(normalizedVelocity <= 0)
        {
            GameController.Instance.gameOver = true;
            print("Game Over");
            return;
        }
    }
}
