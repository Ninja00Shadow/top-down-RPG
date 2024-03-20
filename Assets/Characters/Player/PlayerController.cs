using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;
    
    public SwordAttack swordAttack;

    Vector2 movementInput;
    SpriteRenderer renderer;
    Rigidbody2D rigidbody;
    Animator animator;

    List<RaycastHit2D> castCollisions = new();

    bool canMove = true;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));

                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }

                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            FlipSpriteHorizontally();

            ChangeSwordDirection();

            AnimateMovement(movementInput);
        }
    }

    private void ChangeSwordDirection()
    {
        if (movementInput.x > 0)
        {
            swordAttack.attackDirection = SwordAttack.AttackDirection.right;
        }
        else if (movementInput.x < 0)
        {
            swordAttack.attackDirection = SwordAttack.AttackDirection.left;
        }
        else if (movementInput.y > 0)
        {
            swordAttack.attackDirection = SwordAttack.AttackDirection.up;
        }
        else if (movementInput.y < 0)
        {
            swordAttack.attackDirection = SwordAttack.AttackDirection.down;
        }
    }

    private void AnimateMovement(Vector2 direction)
    {
        animator.SetFloat("horizontal", direction.x * 2);
        animator.SetFloat("vertical", direction.y * 2);
    }

    private bool TryMove(Vector2 direction)
    {
        var count = rigidbody.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
        );
        if (count == 0)
        {
            rigidbody.MovePosition(rigidbody.position + direction * (moveSpeed * Time.fixedDeltaTime));
            return true;
        }

        return false;
    }

    private void FlipSpriteHorizontally()
    {
        renderer.flipX = movementInput.x switch
        {
            < 0 => true,
            > 0 => false,
            _ => renderer.flipX
        };
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }
    
    public void SwordSwing()
    {
        LockMovement();
        swordAttack.Attack();
    }
    
    public void SwordSwingEnd()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}