using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AbstractBullet
{
    [SerializeField] private float force;
    private Vector3 mousePosition;
    private Camera mainCamera;

    public override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
        Invoke("SetInactive", 3);
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
        if(collision.gameObject.CompareTag("Enemy"))
        {
            ComputeFinalDamageDealt();
            collision.gameObject.GetComponent<IDamagable>().TakeDamage((int)FinalDamage);
        }

        SetInactive();
    }
}
