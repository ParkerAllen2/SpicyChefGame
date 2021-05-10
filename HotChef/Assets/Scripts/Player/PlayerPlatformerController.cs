using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPlatformerController : MonoBehaviour
{
    private PlatformerRigidbody2D playerMovement;
    Hitbox hitbox;
    public Attack[] attacks;

    Animator[] animators;
    SpriteRenderer[] spriteRenderers;

    Vector3 MOUSEADJUST = Vector3.up * -1f;

    void Start()
    {
        playerMovement = GetComponentInParent<PlatformerRigidbody2D>();
        animators = GetComponentsInChildren<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        hitbox = GetComponentInChildren<Hitbox>();
    }

    void Update()
    {
        GetMovementInput();
        playerMovement.AnimateCharacter(animators);
        playerMovement.FlipSprite();
        AttackInput();
    }

    private void GetMovementInput()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        playerMovement.DirectionalInput = directionalInput;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMovement.OnJumpInputDown();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerMovement.OnJumpInputUp();
        }
    }

    void AttackInput()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (hitbox.attacking)
        {
            return;
        }
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        string anim = "";
        foreach (Attack att in attacks)
        {
            if (att.CanUse())
            {
                hitbox.StartSwinging(playerMovement.FaceDir.x, att);
                anim = att.animationName;
                break;
            }
        }

        if (!anim.Equals(""))
        {
            animators[0].Play(anim);
            animators[1].Play(anim);
        }
    }

    Vector3 GetMousePosition()
    {
        Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        camPos.z = 0;
        return camPos + MOUSEADJUST;
    }
}
