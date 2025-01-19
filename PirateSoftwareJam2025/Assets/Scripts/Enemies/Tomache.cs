using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomache : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 3f;
    [SerializeField] private float chaseSpeed = 6f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int damage = 2;
    [SerializeField] private float attackCooldown = 2f;
    public float currentSpeed;
    private Transform target;
    private Rigidbody2D rb;
    private bool isChasing = false;
    private bool canAttack = true;
    private float attackTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        FindClosestTarget();
    }

    private void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
            if (Vector2.Distance(transform.position, target.position) <= attackRange && canAttack)
            {
                AttackTarget();
            }
        }
        else
        {
            FindClosestTarget();
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

    private void FindClosestTarget()
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Player");

        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (GameObject clone in clones)
        {
            float distanceToClone = Vector2.Distance(transform.position, clone.transform.position);
            if (distanceToClone < closestDistance)
            {
                closestDistance = distanceToClone;
                closestTarget = clone.transform;
            }
        }

        target = closestTarget;
    }

    private void MoveTowardsTarget()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= detectionRange)
        {
            isChasing = true;
        }

        rb.velocity = (target.position - transform.position).normalized * currentSpeed;

        if(GetComponent<SlowEnemy>().isSlowed == true)
        {
            return;
        }

        currentSpeed = isChasing ? chaseSpeed : initialSpeed;
    }

    private void AttackTarget()
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            bool instantKill = Random.value <= 0.5f;
            if (instantKill)
            {
                Destroy(target.gameObject);
            }
            else
            {
                playerHealth.TakeDamage(damage);
            }
        }

        isChasing = false;
        canAttack = false;
    }
}
