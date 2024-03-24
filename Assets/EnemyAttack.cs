// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Serialization;
//
// public class EnemyAttack : MonoBehaviour
// {
//     public enum AttackDirection
//     {
//         left,
//         right
//     }
//
//     public AttackDirection attackDirection;
//     public float damage = 1;
//
//     private Vector3 _attackOffset;
//
//     public EnemyController enemyController;
//
//     public Collider2D hitbox;
//
//     void Start()
//     {
//         hitbox.enabled = false;
//         _attackOffset = transform.localPosition;
//     }
//
//     public void Attack()
//     {
//         switch (attackDirection)
//         {
//             case AttackDirection.left:
//                 AttackLeft();
//                 break;
//             case AttackDirection.right:
//                 AttackRight();
//                 break;
//             default:
//                 AttackRight();
//                 break;
//         }
//     }
//
//     public void AttackRight()
//     {
//         transform.localPosition = _attackOffset;
//         hitbox.enabled = true;
//     }
//
//     private void AttackLeft()
//     {
//         transform.localPosition = new Vector3(_attackOffset.x * -1, _attackOffset.y);
//         hitbox.enabled = true;
//     }
//
//     public void StopAttack()
//     {
//         hitbox.enabled = false;
//     }
//
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             PlayerController player = other.GetComponent<PlayerController>();
//
//             if (player != null)
//             {
//                 enemyController.AttackAnimation();
//                 Attack();
//             }
//         }
//     }
// }