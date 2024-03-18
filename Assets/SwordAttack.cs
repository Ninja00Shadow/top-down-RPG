using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public enum AttackDirection
    {
        left, right, up, down
    }
    
    public AttackDirection attackDirection;
    
    Vector2 rightAttackOffset;
    Vector2 downAttackOffset;
    Vector2 upAttackOffset;
    Collider2D swordHitbox;
    
    private Vector2 _playerPosition;
    void Start()
    {
        swordHitbox = GetComponent<Collider2D>();
        rightAttackOffset = new Vector2(0.11f, -0.11f);
        downAttackOffset = new Vector2(0.01f, -0.19f);
        upAttackOffset = new Vector2(-0.01f, -0.02f);
    }

    void Update()
    {
        
    }

    public void Attack(Vector2 playerPosition)
    {
        _playerPosition = playerPosition;
        
        switch (attackDirection)
        {
            case AttackDirection.left:
                AttackLeft();
                break;
            case AttackDirection.right:
                AttackRight();
                break;
            case AttackDirection.up:
                AttackUp();
                break;
            case AttackDirection.down:
                AttackDown();
                break;
            default:
                AttackRight();
                break;
        }
    }
    
    private void AttackRight()
    {
        transform.position = _playerPosition + rightAttackOffset;
        swordHitbox.enabled = true;
    }

    private void AttackLeft()
    {
        transform.position = _playerPosition + new Vector2(-rightAttackOffset.x, rightAttackOffset.y);
        swordHitbox.enabled = true;
    }
    
    private void AttackUp()
    {
        transform.position = _playerPosition + upAttackOffset;
        swordHitbox.enabled = true;
    }
    
    private void AttackDown()
    {
        transform.position = _playerPosition + downAttackOffset;
        swordHitbox.enabled = true;
    }
    
    private void StopAttack()
    {
        swordHitbox.enabled = false;
    }
}
