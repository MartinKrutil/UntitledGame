using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject collisionEffect;

    [SerializeField]
    private float speed;

    private void Start(){ }

    private void Explode()
    {
        Destroy(gameObject);
        GameObject effect = Instantiate(collisionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);       
    }

    private void OnCollisionEnter2D(Collision2D collision) => Explode();

}
