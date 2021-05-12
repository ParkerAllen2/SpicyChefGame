using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBar : SliderValidator
{
    ParticleSystem particles;
    public ScoreCounter scoreCounter;
    public Rigidbody2D ball;
    public Gradient gradient;

    public float max;
    public float min;
    float current;

    protected override void Start()
    {
        base.Start();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        current = Mathf.Max(0, (ball.velocity.magnitude - min) / max);
        MoveHandle(current);
        fill.color = gradient.Evaluate(current);
        scoreCounter.AddPoints(current);
    }
}
