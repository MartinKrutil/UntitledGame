using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject collisionEffect;
    [SerializeField] private LayerMask hitMask;

    [SerializeField] private float lifeTime;

    private float damage;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            print(damage);
            collision.collider.GetComponent<EnemyHealthController>().TakeDamage(this.damage);
            Explode();
        }
    }
    
    private void Explode()
    {      
        GameObject effect = Instantiate(collisionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        Destroy(gameObject);
    }

    //public void SetDamage(float damage) => this.damage = damage; 
    public void SetDamage(float damage)
    {
        this.damage = damage;
        print("damage set");
        print(damage);
    }
}
