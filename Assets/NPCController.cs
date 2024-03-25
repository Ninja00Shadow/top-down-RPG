using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float moveSpeed = 0.75f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rigidbody;
    Animator animator;

    List<RaycastHit2D> castCollisions = new();
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        StartCoroutine(MoveRandomly());
    }

    private void FixedUpdate()
    {
        print(movementInput);
        
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

        AnimateMovement(movementInput);
    }
    
    IEnumerator MoveRandomly()
    {
        while (true)
        {
            movementInput = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }
    

    private void AnimateMovement(Vector2 direction)
    {
        animator.SetFloat("horizontal", direction.x * 10);
        animator.SetFloat("vertical", direction.y * 10);
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
}