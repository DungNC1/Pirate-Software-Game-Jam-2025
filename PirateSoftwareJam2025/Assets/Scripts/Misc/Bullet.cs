using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float force;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("DestroySelf", 3);
    }

    public void SetDirection(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        rb.velocity = direction.normalized * force;
        float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }

        DestroySelf();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
