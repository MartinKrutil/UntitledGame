using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private float health;

    private void Start() => health = maxHealth;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health == 0)
            Die();
    }
    private void Die() => Destroy(gameObject);
}
