using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    ParticleSystem ballParticles;
    Rigidbody2D rb;
    public Gradient cold, hot;
    public float maxVelocity;
    bool particlesPlaying;


    private void Start()
    {
        ballParticles = GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }

    public void UpdateBall(bool isHighSpeed, bool isLowSpeed)
    {
        var col = ballParticles.colorOverLifetime;
        if (isHighSpeed)
        {
            col.color = hot;
            if (!particlesPlaying)
            {
                particlesPlaying = true;
                ballParticles.Play();
            }
            return;
        }
        if (isLowSpeed)
        {
            col.color = cold;
            if (!particlesPlaying)
            {
                particlesPlaying = true;
                ballParticles.Play();
            }
            return;
        }
        particlesPlaying = false;
        ballParticles.Stop();
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }
}
