using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPlatformerController : MonoBehaviour
{
    private PlatformerRigidbody2D playerMovement;

    Animator[] animators;
    SpriteRenderer[] spriteRenderers;

    Vector3 MOUSEADJUST = Vector3.up * -1f;

    void Start()
    {
        playerMovement = GetComponentInParent<PlatformerRigidbody2D>();
        animators = GetComponentsInChildren<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        GetMovementInput();
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            AttackInput();
        }
        else
        {
            playerMovement.DirectionalInput = Vector3.zero;
        }
        playerMovement.AnimateCharacter(animators);
        playerMovement.FlipSprite(spriteRenderers);
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
        string anim = "";

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
