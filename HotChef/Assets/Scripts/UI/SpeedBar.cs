using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBar : SliderValidator
{
    ParticleSystem barParticles;

    //public ScoreCounter scoreCounter;
    public Rigidbody2D ball;
    public Gradient gradient;
    public ParticleSystem smoke;

    protected override void Start()
    {
        base.Start();
        barParticles = GetComponentInChildren<ParticleSystem>();
    }

    public void UpdateBar(float value)
    {
        MoveHandle(value);
        fill.color = gradient.Evaluate(value);
        if(value < 1)
        {
            smoke.Stop();
            return;
        }
        if (!smoke.isPlaying)
        {
            smoke.Play();
        }
    }
}
