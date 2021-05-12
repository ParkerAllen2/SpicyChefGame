using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float timeStartDeath, deathTime;

    public float speed;
    [Range(-1.0f, 1.0f)]
    public float circleWeight;
    [Range(-1.0f, 1.0f)]
    public float noiseWeight;
    [Range(-1.0f, 1.0f)]
    public float ballWeight;
    [Range(-1.0f, 1.0f)]
    public float playerWeight;

    float rotationDirection;
    Vector3 randomDirection;
    Rigidbody2D rb;
    Transform player, ball;
    ParticleSystem[] particles;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ball = GameObject.FindGameObjectWithTag("Ball").transform;

        particles = GetComponentsInChildren<ParticleSystem>();

        rotationDirection = (Random.Range(0, 2) == 0) ? -1 : 1;

        StartCoroutine(WaitToChangeNoise());
        StartCoroutine(SelfDestruct());
    }

    private void FixedUpdate()
    {
        float t = Time.time;
        Vector3 position = transform.position;

        Vector3 circlef = new Vector3(Mathf.Cos(t), Mathf.Sin(t)) * rotationDirection * circleWeight;
        Vector3 noisef = randomDirection * noiseWeight;
        Vector3 playerf = (player.position - position).normalized * playerWeight;
        Vector3 ballf = (ball.position - position).normalized * ballWeight;

        Vector3 velocity = (circlef + noisef + playerf + ballf).normalized * speed;

        rb.velocity = velocity;
    }

    IEnumerator WaitToChangeNoise()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            randomDirection = Random.insideUnitCircle;
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeStartDeath);
        foreach(ParticleSystem ps in particles)
        {
            ps.Stop();
        }
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(deathTime);
        Destroy(this.gameObject);
    }
}
