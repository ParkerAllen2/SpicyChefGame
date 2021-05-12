using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBar : SliderValidator
{
    ParticleSystem barParticles;

    //public ScoreCounter scoreCounter;
    public Rigidbody2D ball;
    public Gradient gradient;

    protected override void Start()
    {
        base.Start();
        barParticles = GetComponentInChildren<ParticleSystem>();
    }

    public void UpdateBar(float value)
    {
        MoveHandle(value);
        fill.color = gradient.Evaluate(value);
    }
}
