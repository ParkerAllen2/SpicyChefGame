using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowArea : FaceRenderer
{
    public Sprite hitFace;
    [Range(0.0f, 1.0f)]
    public float slowRate;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();
            ballRb.velocity *= (1 - slowRate);
            ChangeFace(hitFace, .1f);
        }
    }
}
