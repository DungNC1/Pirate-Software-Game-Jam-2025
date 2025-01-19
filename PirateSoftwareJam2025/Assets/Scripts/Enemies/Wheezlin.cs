using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheezlin : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int damage = 2;
    [SerializeField] private float attackCooldown = 1.5f;
    private Transform player;
    private Rigidbody2D rb;
    private bool canAttack = true;
    private float attackTimer;

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
            if (Vector2.Distance(transform.position, player.position) <= attackRange && canAttack)
            {
                AttackPlayer();
            }
        }

        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0f;
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            canAttack = false;
        }
    }
}
