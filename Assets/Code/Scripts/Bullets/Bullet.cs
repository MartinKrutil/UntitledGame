using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject collisionEffect;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float speed;

    private void Start() { }

    private void Explode()
    {      
        GameObject effect = Instantiate(collisionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<EnemyHealthController>().TakeDamage(this.damage);
            Explode();
        }           
    } 
}
