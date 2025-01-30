using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBullet : AbstractBullet
{
    [SerializeField] private float force;
    [SerializeField] private float lifetime = 5f;
    private Vector3 mousePosition;
    private Camera mainCamera;

    public override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
        Invoke("SetInactive", lifetime);
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
            SlowEnemy slowEnemy = collision.gameObject.GetComponent<SlowEnemy>();
            ComputeFinalDamageDealt();
            CreateIndicator((int)FinalDamage);
            collision.gameObject.GetComponent<IDamagable>().TakeDamage((int)FinalDamage);

            if (slowEnemy != null)
            {
                StartCoroutine(slowEnemy.SlowDown());
            }
        }

        SetInactive();
    }
}
