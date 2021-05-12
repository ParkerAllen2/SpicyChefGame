using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerRigidbody2D : MonoBehaviour
{
    CharacterCollider2D characterMovement;

    private Rigidbody2D rb;

    Collider2D characterCollider;
    

    [SerializeField] float sprintSpeed = 10;
    private float moveSpeed;

    [SerializeField] float maxJumpHeight = 5, minJumpHeight = 3;
    float maxJumpVelocity, minJumpVelocity;

    [SerializeField] float timeToJumpApex = .5f;
    float gravity;

    [SerializeField] float accelerationTimeAir = 1, accelerationTimeGround = .1f;
    [SerializeField] float stopDeacceleratingX = .5f;

    public float knockBackResist;
    Vector3 knockBack;

    public Vector3 velocity;
    float velocityXSmoothing;

    Vector2 directionalInput;
    Vector2 faceDir;
    bool flip;

    bool sprinting, jumping, falling, idling;
    bool onGround;

    public float timeScale = 1;

    public void Awake()
    {
        characterMovement = GetComponent<CharacterCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<Collider2D>();

        faceDir = Vector2.right;

        CalculatePhysics();
    }

    //caculates gravity, max jump velocity and min jump velocity
    public void CalculatePhysics()
    {
        gravity = -2 * MaxjumpHeight / Mathf.Pow(timeToJumpApex, 2);

        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        moveSpeed = sprintSpeed;
    }

    public void FixedUpdate()
    {
        onGround = characterMovement.sides.below;
        SetCharacterState();
        CalculateVelocity();
        GetMovement();
        PostMovementUpdate();
    }

    public void GetMovement()
    {
        GetMovement(velocity * Time.deltaTime * timeScale);
    }

    public void GetMovement(Vector3 v)
    {
        rb.transform.position += characterMovement.Move(v, directionalInput);
    }

    public void SetCharacterState()
    {

        ResetPlayerStates();

        if (velocity.y > 0)
        {
            Jumping = true;
        }
        else if (velocity.y < 0 && !onGround)
        {
            Falling = true;
        }
        else if (onGround && velocity.x != 0 && directionalInput.x != 0)
        {
            Sprinting = true;
            moveSpeed = sprintSpeed;
        }
        else
        {
            Idling = true;
        }
    }

    public void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed + knockBack.x;

        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (characterMovement.sides.below) ? accelerationTimeGround : accelerationTimeAir);
        velocity.y += gravity * Time.fixedDeltaTime * timeScale + knockBack.y;

        knockBack = Vector3.Max(KnockBack * knockBackResist, Vector3.zero);
    }

    //when jumping button is pressed return velocity for wall jumping or max jump velocity
    public void OnJumpInputDown()
    {
        if (onGround)
        {
            velocity.y = maxJumpVelocity;
        }
    }

    //when jump button is released try to lower the velocity y to min jump velocity
    //this make it jump higher the longer jump is held
    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
            velocity.y = minJumpVelocity;
    }

    public void AnimateCharacter(Animator[] anims, bool canIntr = true)
    {
        foreach(Animator anim in anims)
        {
            anim.SetBool("Running", sprinting && canIntr);
            anim.SetBool("Falling", falling && canIntr);
            anim.SetBool("Jumping", jumping && canIntr);
        }
    }

    public void PostMovementUpdate()
    {
        if (characterMovement.sides.below || characterMovement.sides.above)
        {
            if (characterMovement.sides.slidingDownMaxSlope)
                velocity.y += characterMovement.sides.slopeNormal.y * -gravity * Time.deltaTime * timeScale;

            else
                velocity.y = 0;
        }

        if (directionalInput.x == 0 && Mathf.Abs(velocity.x) < stopDeacceleratingX)
        {
            velocity.x = 0;
        }
    }

    public void ResetPlayerStates()
    {
        Sprinting = Jumping = Falling = Idling = false;
    }

    public void FlipSprite()
    {
        if(directionalInput.x == 0)
        {
            return;
        }
        FlipSprite(velocity.x < 0);
    }

    public void FlipSprite(bool flip)
    {
        faceDir = flip ? Vector2.left : Vector2.right;
        transform.rotation = flip ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }

    /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Getters/Setters ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

    public CharacterCollider2D GetCharacterMovement()
    {
        return characterMovement;
    }

    public Vector3 DirectionalInput
    {
        get { return directionalInput; }
        set { directionalInput = value; }
    }

    public Vector2 FaceDir
    {
        get { return faceDir; }
    }

    public float SprintSpeed
    {
        get { return sprintSpeed; }
        set { sprintSpeed = value; }
    }

    public float AccelerationTimeAir
    {
        get { return accelerationTimeAir; }
        set { accelerationTimeAir = value; }
    }

    public float AccelerationTimeGround
    {
        get { return accelerationTimeGround; }
        set { accelerationTimeGround = value; }
    }

    public float StopDeacceleratingX
    {
        get { return stopDeacceleratingX; }
        set { stopDeacceleratingX = value; }
    }

    public float MaxjumpHeight
    {
        get { return maxJumpHeight; }
        set
        {
            maxJumpHeight = value;
            CalculatePhysics();
        }
    }

    public float MinJumpHieght
    {
        get { return minJumpHeight; }
        set
        {
            minJumpHeight = value;
            CalculatePhysics();
        }
    }

    public float TimeToJumpApex
    {
        get { return timeToJumpApex; }
        set
        {
            timeToJumpApex = value;
            CalculatePhysics();
        }
    }

    public bool Sprinting
    {
        get { return sprinting; }
        set { sprinting = value; }
    }

    public bool Jumping
    {
        get { return jumping; }
        set { jumping = value; }
    }

    public bool Idling
    {
        get { return idling; }
        set { idling = value; }
    }

    public bool Falling
    {
        get { return falling; }
        set { falling = value; }
    }

    public bool OnGround
    {
        get { return onGround; }
    }

    public Vector3 KnockBack
    {
        get { return knockBack; }
        set { knockBack += value; }
    }
}
