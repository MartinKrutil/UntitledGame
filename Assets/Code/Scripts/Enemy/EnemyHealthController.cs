using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int health;

    private void Start() => health = maxHealth;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health == 0)
            Die();
    }
    private void Die() => Destroy(gameObject);
}
