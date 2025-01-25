using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AbstractBullet
{
    [SerializeField] private float force;

    public override void Awake()
    {
        base.Awake();
        Invoke("SetInactive", 3);
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
            collision.gameObject.GetComponent<IDamagable>().TakeDamage((int)FinalDamage);
        }

        SetInactive();
    }
}
