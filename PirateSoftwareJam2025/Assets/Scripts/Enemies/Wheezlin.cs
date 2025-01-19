using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheezlin : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int damage = 2;
    [SerializeField] private float attackCooldown = 1.5f;
    private Transform closestClone;
    private Rigidbody2D rb;
    private bool canAttack = true;
    private float attackTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        FindClosestClone();
    }

    private void Update()
    {
        if (closestClone != null)
        {
            MoveTowardsClone();
            if (Vector2.Distance(transform.position, closestClone.position) <= attackRange && canAttack)
            {
                AttackClone();
            }
        }
        else
        {
            FindClosestClone();
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

    private void FindClosestClone()
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

        closestClone = closestTarget;
    }

    private void MoveTowardsClone()
    {
        Vector2 direction = (closestClone.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void AttackClone()
    {
        PlayerHealth playerHealth = closestClone.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            canAttack = false;
        }
    }

}
