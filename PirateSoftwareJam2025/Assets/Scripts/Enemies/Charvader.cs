using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charvader : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private float chargeSpeed = 10f;
    [SerializeField] private float detectionRange = 7f;
    [SerializeField] private int damage = 100;
    [SerializeField] private float chargeCooldown = 3f;
    private Transform player;
    private Rigidbody2D rb;
    private bool isCharging = false;
    private bool canCharge = true;
    private bool canAttack = true;
    private float chargeTimer;
    private float attackTimer;
    private Vector2 chargeDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
            if (Vector2.Distance(transform.position, player.position) <= detectionRange && canCharge && !isCharging)
            {
                StartCoroutine(ChargeTowardsPlayer());
            }
        }

        if (!canCharge)
        {
            chargeTimer += Time.deltaTime;
            if (chargeTimer >= chargeCooldown)
            {
                canCharge = true;
                chargeTimer = 0f;
            }
        }

        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= chargeCooldown)
            {
                canAttack = true;
                attackTimer = 0f;
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        if (!isCharging)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    private IEnumerator ChargeTowardsPlayer()
    {
        isCharging = true;
        chargeDirection = (player.position - transform.position).normalized;
        rb.velocity = chargeDirection * chargeSpeed;
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        isCharging = false;
        canCharge = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            isCharging = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canAttack)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                canAttack = false;
            }
        }
    }
}
