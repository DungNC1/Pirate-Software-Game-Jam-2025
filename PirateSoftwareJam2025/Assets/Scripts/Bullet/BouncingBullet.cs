using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BouncingBullet : AbstractBullet
{
    [SerializeField] private float force;
    [SerializeField] private int maxBounces = 5;
    private int bounceCount = 0;
    private float lifetime = 5f;
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
        bounceCount++;

        if (bounceCount >= maxBounces)
        {
            SetInactive();
        }
        
        if(collision.gameObject.CompareTag("Enemy"))
        {
            ComputeFinalDamageDealt();
            CreateIndicator((int)FinalDamage);
            collision.gameObject.GetComponent<IDamagable>().TakeDamage((int)FinalDamage);
        }
    }
}
