using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlord : MonoBehaviour
{
    public BallController ball;
    public SpeedBar odometer;
    public TotalScore totalScore;
    public ComboScore comboScore;

    public float highSpeed, lowSpeed;
    public float max, min;
    float normalizedVelocity;

    bool gameOver;
    bool isHighSpeed, isLowSpeed;

    private void Start()
    {
        
    }

    private void Update()
    {
        normalizedVelocity = Mathf.Max(0, (ball.GetVelocity().magnitude - min) / max);
        isHighSpeed = normalizedVelocity >= highSpeed;
        isLowSpeed = normalizedVelocity < lowSpeed;

        ball.UpdateBall(isHighSpeed, isLowSpeed);
        odometer.UpdateBar(normalizedVelocity);
    }
}
