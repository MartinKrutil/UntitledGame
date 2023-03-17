using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject collisionEffect;

    //[Range(1, 10)] [SerializeField] private float speed;

    [Range(1, 10)][SerializeField] private int damage; 
    [Range(1, 10)][SerializeField] private float lifeTime;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    
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
            collision.collider.GetComponent<EnemyHealthController>().TakeDamage(damage);
            Explode();
        }           
    } 
}
