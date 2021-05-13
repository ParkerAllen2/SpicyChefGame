using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPlatformerController : MonoBehaviour
{
    private PlatformerRigidbody2D playerMovement;
    Hitbox hitbox;
    public Attack[] attacks;
    public float swingKnockback;

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
        if (GameController.Instance.gameOver)
        {
            playerMovement.DirectionalInput = Vector3.zero;
            playerMovement.AnimateCharacter(animators);
            return;
        }
        GetMovementInput();
        playerMovement.AnimateCharacter(animators);
        if (!hitbox.attacking)
        {
            playerMovement.FlipSprite();
            AttackInput();
        }
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
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            return;
        }
        if (Input.GetMouseButton(1) && attacks[1].CanUse())
        {
            PlayAttack(attacks[1]);
            return;
        }

        foreach (Attack att in attacks)
        {
            if (att.CanUse())
            {
                PlayAttack(att);
                break;
            }
        }
    }

    void PlayAttack(Attack att)
    {
        if (playerMovement.DirectionalInput.x != 0)
        {
            playerMovement.FlipSprite(playerMovement.DirectionalInput.x < 0);
        }

        hitbox.StartSwinging(playerMovement.FaceDir.x, att);
        playerMovement.KnockBack = playerMovement.FaceDir * swingKnockback;

        animators[0].Play("Player_Attack_" + att.animationName);
        if (playerMovement.Idling)
        {
            animators[1].Play("Player_L_Attack_" + att.animationName);
        }
    }

    Vector3 GetMousePosition()
    {
        Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        camPos.z = 0;
        return camPos + MOUSEADJUST;
    }
}
