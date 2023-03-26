using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject collisionEffect;

    [SerializeField] private float lifeTime;

    private float damage;

    private void Start() => Destroy(gameObject, lifeTime);

    public void SetDamage(float damage) => this.damage = damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<EnemyHealthController>().TakeDamage(this.damage);
            Explode();
        }
        if (collision.collider.CompareTag("Wall"))
        {
            Explode();
        }
    }
    
    private void Explode()
    {      
        GameObject effect = Instantiate(collisionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        Destroy(gameObject);
    }
}
