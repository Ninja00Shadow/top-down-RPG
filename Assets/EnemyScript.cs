using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
            }
        }
        get => health;
    }

    public float health = 1f;

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    private void Defeated()
    {
        Destroy(gameObject);
    }
}
