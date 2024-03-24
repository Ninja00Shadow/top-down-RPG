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
    public float damage = 3;

    private readonly Vector2 _rightAttackOffset = new Vector2(0.11f, -0.11f);
    private readonly Vector2 _downAttackOffset = new Vector2(0.01f, -0.19f);
    private readonly Vector2 _upAttackOffset = new Vector2(-0.01f, -0.02f);
    
    public Collider2D swordHitbox;
    
    private AudioSource audioSource;
    public AudioClip swordSwingSound;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        swordHitbox.enabled = false;
    }

    public void Attack()
    {
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
        audioSource.PlayOneShot(swordSwingSound, 0.5f);
    }
    
    private void AttackRight()
    {
        transform.localPosition = _rightAttackOffset;
        swordHitbox.enabled = true;
    }

    private void AttackLeft()
    {
        transform.localPosition = new Vector2(-_rightAttackOffset.x, _rightAttackOffset.y);
        swordHitbox.enabled = true;
    }
    
    private void AttackUp()
    {
        transform.localPosition = _upAttackOffset;
        swordHitbox.enabled = true;
    }
    
    private void AttackDown()
    {
        transform.localPosition = _downAttackOffset;
        swordHitbox.enabled = true;
    }
    
    public void StopAttack()
    {
        swordHitbox.enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Hit something");
        if (other.CompareTag("Enemy"))
        {
            EnemyScript enemy = other.GetComponent<EnemyScript>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
