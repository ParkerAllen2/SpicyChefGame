using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    BoxCollider2D wall;
    ParticleSystem[] particles;
    public bool coldPlaying, hotPlaying;

    void Awake()
    {
        wall = GetComponent<BoxCollider2D>();
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    public void SetSize(Vector2 size)
    {
        wall.size = size;
        foreach(ParticleSystem ps in particles)
        {
            var shape = ps.shape.radius;
            shape = Mathf.Max(size.x, size.y);
        }
    }

    public void UpdateWall(bool isHighSpeed, bool isLowSpeed)
    {
        if(isHighSpeed == false && isLowSpeed == false)
        {
            hotPlaying = coldPlaying = false;
            foreach (ParticleSystem ps in particles)
                ps.Stop();
            return;
        }
        if (isHighSpeed && !hotPlaying)
        {
            hotPlaying = true;
            particles[0].Play();
            particles[1].Stop();
            return;
        }
        if (isLowSpeed && !coldPlaying)
        {
            coldPlaying = true;
            particles[1].Play();
            particles[0].Stop();
            return;
        }
    }
}
