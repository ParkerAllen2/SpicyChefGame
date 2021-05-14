using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    BoxCollider2D hitbox;
    public Transform pivot;

    Attack attack;
    PlatformerRigidbody2D direction;
    [HideInInspector] public bool attacking;

    private void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
        direction = GetComponentInParent<PlatformerRigidbody2D>();
        hitbox.enabled = false;
    }
    
    //Anyways I 
    public void StartSwinging(Attack att)
    {
        attacking = true;
        attack = att;
        hitbox.enabled = true;
        attack.SetCooldown();
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
            Vector3 force = CalculateForce(ballRb);
            ballRb.velocity = force;
            hitbox.enabled = false;
        }
    }

    public Vector3 CalculateForce(Rigidbody2D ballPos)
    {
        Vector3 dir = Quaternion.Euler(0, 0, attack.offsetAngle) * ((Vector3)ballPos.position - pivot.position).normalized;
        dir.x = Mathf.Abs(dir.x) * direction.FaceDir.x;

        return dir * attack.magnitude + dir * (ballPos.velocity.magnitude * .5f);
    }
}
