using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;

    public SwordAttack swordAttack;

    private Vector2 movementInput;
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody;
    private Animator animator;

    List<RaycastHit2D> castCollisions = new();

    bool canMove = true;

    public HealthMoneyBar healthMoneyBar;
    public float health = 3f;
    public int coins = 0;
    
    public bool invincible = false;
    
    private static bool playerExists = false;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
        var count = _rigidbody.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
        );
        if (count == 0)
        {
            _rigidbody.MovePosition(_rigidbody.position + direction * (moveSpeed * Time.fixedDeltaTime));
            return true;
        }

        return false;
    }

    private void FlipSpriteHorizontally()
    {
        _renderer.flipX = movementInput.x switch
        {
            < 0 => true,
            > 0 => false,
            _ => _renderer.flipX
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

    public void TakeDamage(float damage)
    {
        if (health > 0 && !invincible)
        {
            health -= damage;
            healthMoneyBar.health = (int)health;
            healthMoneyBar.UpdateHealth();
        }
        print(health);

        if (!(health <= 0)) return;
        invincible = true;
        health = 3f;
        healthMoneyBar.health = (int)health;
        healthMoneyBar.UpdateHealth();
        animator.SetTrigger("death");
        LockMovement();
    }

    public void Defeated()
    {
        print("Invoked");
        transform.position = new Vector3(0, 0, 0);
        invincible = false;
        UnlockMovement();
    }
    
    public void GetCoins(int money)
    {
        coins += money;
        healthMoneyBar.UpdateMoney(money);
    }
}