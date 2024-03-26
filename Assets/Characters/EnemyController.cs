using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float range = 0.75f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;

    private Vector2 movementDirection;
    private SpriteRenderer renderer;
    private Rigidbody2D rigidbody;
    private Animator animator;

    private List<RaycastHit2D> castCollisions = new();

    private Transform target;
    private bool canMove = true;

    public float Health
    {
        set
        {
            health = value;
            if (health <= 0 && !invincible)
            {
                animator.SetTrigger("death");
                invincible = true;
                damage = 0;
            }
        }
        get => health;
    }

    public float health = 1f;
    public float damage = 1f;
    public bool invincible = false;
    
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        target = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) <= range)
        {
            movementDirection = (target.position - transform.position).normalized;
            FollowPlayer();
        }
        else
        {
            movementDirection = Vector2.zero;
            FollowPlayer();
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public void Defeated()
    {
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        player.GetCoins(1);
        
        Destroy(gameObject);
    }

    public void FollowPlayer()
    {
        if (!canMove) return;
        if (!target) return;
        if (movementDirection != Vector2.zero)
        {
            bool success = TryMove(movementDirection);

            if (!success)
            {
                success = TryMove(new Vector2(movementDirection.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementDirection.y));
                }
            }

            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        FlipSpriteHorizontally();
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
        renderer.flipX = movementDirection.x switch
        {
            < 0 => true,
            > 0 => false,
            _ => renderer.flipX
        };
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            
            player.TakeDamage(damage);
        }
    }
}