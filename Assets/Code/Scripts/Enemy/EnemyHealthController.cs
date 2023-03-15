using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int health;

    private void Start() => this.health = this.maxHealth;

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health == 0)
            Die();
    }
    private void Die() => Destroy(gameObject);
}
