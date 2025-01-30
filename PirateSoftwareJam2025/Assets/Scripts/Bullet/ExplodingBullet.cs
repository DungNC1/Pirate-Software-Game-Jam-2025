using UnityEngine;
using System.Collections;

public class ExplodingBullet : AbstractBullet
{
    [SerializeField] private float explosionDamage = 10f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float force = 10f;
    private Vector3 mousePosition;
    private Camera mainCamera;

    public override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        Vector3 rotation = transform.position - mousePosition;
        rb.velocity = new Vector2(Direction.x, Direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Explode();
            SetInactive();
        }
    }

    private void Explode()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                IDamagable enemyHealth = hitCollider.GetComponent<IDamagable>();
                if (enemyHealth != null)
                {
                    ComputeFinalDamageDealt();
                    enemyHealth.TakeDamage((int)explosionDamage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
