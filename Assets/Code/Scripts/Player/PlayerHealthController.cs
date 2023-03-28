using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private float invincibilityTime;

    [SerializeField] private AudioClip damageSFX;

    private SpriteRenderer spriteRenderer;

    private bool canTakeDamage = true;

    private void Start()
    {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        HUDManager.instance.DisplayHearts(health);
    } 

    private void Die() => Destroy(gameObject);

    public void TakeDamage(int damage)
    {
        if (!canTakeDamage) return;

        this.health -= damage;

        HUDManager.instance.DisplayHearts(health);
        SoundManager.instance.PlaySound(damageSFX);

        if (this.health <= 0)
        {
            Die();
            return;
        }
                              
        BecomeInvincible();
    }

    private async void BecomeInvincible()
    {
        canTakeDamage = false;
        spriteRenderer.color = Color.red;

        await Task.Delay((int)(invincibilityTime * 1000));

        canTakeDamage = true;
        spriteRenderer.color = Color.white;
    }
}
