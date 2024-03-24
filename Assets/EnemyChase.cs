using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    float moveSpeed = 0.5f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;
    Transform target;

    public EnemyScript enemy;

    private void FixedUpdate()
    {
        if (target)
        {
            // float step = moveSpeed * Time.deltaTime;
            // transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            Vector2 direction = (target.position - transform.position).normalized;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }
}
