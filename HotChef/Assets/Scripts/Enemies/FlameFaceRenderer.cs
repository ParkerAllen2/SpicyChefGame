using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameFaceRenderer : FaceRenderer
{
    public Sprite hitFace;
    public float hitDuration;

    protected override void Start()
    {
        base.Start();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ball"))
        {
            ChangeFace(hitFace, hitDuration);
        }
    }
}
