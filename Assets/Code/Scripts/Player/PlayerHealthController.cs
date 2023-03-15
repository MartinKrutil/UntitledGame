using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int health;

    [SerializeField]
    private float invincibilityTime;

    private float timer = 0;
    private bool canTakeDamage = true;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        this.health = this.maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    } 

    private void Die() => Destroy(gameObject);
    public void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;

        this.health -= damage;

        if (this.health == 0)
            Die();

        InvokeRepeating("BecomeInvincible", 0f, 1f);
    }
    private void BecomeInvincible()
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            spriteRenderer.color = Color.red;            
        }      

        if (timer == invincibilityTime)
        {
            CancelInvoke("BecomeInvincible");
            canTakeDamage = true;
            timer = 0;
            spriteRenderer.color = Color.white;
            return;
        }

        timer++;
    }
}
