using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBullet : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float lifetime = 5f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        rb.velocity = direction.normalized * force;
        float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
        Invoke("Despawn", lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SlowEnemy slowEnemy = collision.gameObject.GetComponent<SlowEnemy>();
            collision.gameObject.GetComponent<IDamagable>().TakeDamage(1);

            if (slowEnemy != null)
            {
                StartCoroutine(slowEnemy.SlowDown());
            }
        }

        Despawn();
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
