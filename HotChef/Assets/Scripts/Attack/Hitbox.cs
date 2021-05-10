using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    BoxCollider2D hitbox;
    PlayerPlatformerController controller;
    Transform pivot;

    Attack attack;
    float direction;
    [HideInInspector] public bool attacking;

    private void Start()
    {
        pivot = transform.parent;
        controller = pivot.GetComponent<PlayerPlatformerController>();
        hitbox = GetComponent<BoxCollider2D>();
        hitbox.enabled = false;
    }
    
    //Anyways I 
    public void StartSwinging(float dir, Attack att)
    {
        attacking = true;
        direction = dir;
        attack = att;
        hitbox.enabled = true;
        StartCoroutine(Swing());
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(attack.swingTime);
        hitbox.enabled = false;
        yield return new WaitForSeconds(attack.stunTime);
        attacking = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = other.GetComponent<Rigidbody2D>();
            Vector3 force = CalculateForce(ballRb.position);
            ballRb.velocity = Vector2.zero;
            ballRb.AddForce(force);

            hitbox.enabled = false;
            attack.SetCooldown();
            print("hit");
        }
    }

    public Vector3 CalculateForce(Vector3 ballPos)
    {
        Vector3 dir = Quaternion.Euler(0, 0, attack.offsetAngle) * (ballPos - pivot.position).normalized;
        dir.x = Mathf.Abs(dir.x) * direction;

        return dir * attack.magnitude;
    }
}
