using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngineInternal;

public class BulletTrail : MonoBehaviour
{
    [SerializeField] GameObject collisionEffect;
    private Vector2 startPosition, endPosition;

    private float distance = 0;
    private float speed = 10f;

    private void Awake() => startPosition = transform.position;

    public async void ShootBullet(RaycastHit2D hit, float damage)
    {
        if(hit.collider == null) return;

        endPosition = hit.point;

        while (distance < 1)
        {
            transform.position = Vector2.Lerp(startPosition, endPosition, distance);
            distance += Time.fixedDeltaTime * speed / Vector2.Distance(startPosition, endPosition);
            await Task.Yield();
        }

        if (hit.collider != null && hit.collider.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemyHealthController))
            enemyHealthController.TakeDamage(damage);

        Destroy(gameObject);
        GameObject effect = Instantiate(collisionEffect, endPosition, Quaternion.identity);
        Destroy(effect, 0.1f);
    }
}
