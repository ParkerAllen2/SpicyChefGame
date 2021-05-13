using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BallController : MonoBehaviour
{
    ParticleSystem ballParticles;
    Light2D ballLight;
    Rigidbody2D rb;
    public Gradient cold, hot;
    public Gradient lightGradient;

    bool particlesPlaying;

    public float maxSpeed;
    public float extraSpeed;

    private void Start()
    {
        ballParticles = GetComponentInChildren<ParticleSystem>();
        ballLight = GetComponentInChildren<Light2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        float magDiff = rb.velocity.magnitude - maxSpeed;
        if (magDiff > 0)
        {
            extraSpeed += magDiff;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        else if(extraSpeed > 0)
        {
            float newMag = Mathf.Min(extraSpeed, Mathf.Abs(magDiff));
            velocity = velocity.normalized * (velocity.magnitude + newMag);
            extraSpeed -= newMag;
        }
        rb.velocity = velocity;
    }

    public void UpdateBall(bool isHighSpeed, bool isLowSpeed, float velocity)
    {
        Color color = lightGradient.Evaluate(velocity);
        ballLight.color = color;
        ballLight.pointLightOuterRadius = color.a;
        if (isHighSpeed == false && isLowSpeed == false)
        {
            particlesPlaying = false;
            ballParticles.Stop();
            return;
        }
        var col = ballParticles.colorOverLifetime;
        col.color = isHighSpeed ? hot : cold;
        if (!particlesPlaying)
        {
            particlesPlaying = true;
            ballParticles.Play();
        }
    }

    public float GetSpeed()
    {
        return rb.velocity.magnitude + extraSpeed;
    }
}
