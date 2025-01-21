using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBullet : AbstractBullet
{
    [SerializeField] private float force;
    [SerializeField] private int maxBounces = 5;
    private int bounceCount = 0;
    private float lifetime = 5f;

    public override void Awake()
    {
        base.Awake();
        Invoke("SetInactive", lifetime);
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
        bounceCount++;

        if (bounceCount >= maxBounces)
        {
            SetInactive();
        }
        
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<IDamagable>().TakeDamage(1);
        }
    }
}
