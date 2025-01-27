using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phlennon : AbstractEnnemy
{
    [SerializeField] private float shootRange = 6f;
    [SerializeField] private float retreatRange = 3f;
    [SerializeField] private int phlegmDamage = 20;
    [SerializeField] private float slowDuration = 2f;
    [SerializeField] private float slowFactor = 0.5f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private GameObject phlegmBulletPrefab;
    [SerializeField] private Transform shootPoint;
    private Rigidbody2D rb;
    private bool canAttack = true;
    private float attackTimer;
    private Transform closestClone;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Start()
    {
        base.Start();
        FindClosestClone();
    }

    protected override void Update()
    {
        base.Update();
        if (closestClone != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, closestClone.position);

            if (distanceToPlayer <= shootRange && canAttack)
            {
                ShootPhlegm();
            }

            if (distanceToPlayer < retreatRange)
            {
                MoveAwayFromPlayer();
            }
            else if (distanceToPlayer > shootRange)
            {
                MoveTowardsPlayer();
            }

            RotatePhlegmBullet();
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
        float minDistance = Mathf.Infinity;

        foreach (GameObject clone in clones)
        {
            float distance = Vector3.Distance(transform.position, clone.transform.position);
            if (distance < minDistance)
            {
                closestClone = clone.transform;
                minDistance = distance;
            }
        }
    }

    private void ShootPhlegm()
    {
        GameObject phlegmBullet = Instantiate(phlegmBulletPrefab, shootPoint.position, Quaternion.identity);
        Vector2 direction = (closestClone.position - shootPoint.position).normalized;
        phlegmBullet.GetComponent<Rigidbody2D>().velocity = direction * 10f;
        canAttack = false;
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (closestClone.position - transform.position).normalized;
        rb.velocity = direction * FinalSpeed;
    }

    private void MoveAwayFromPlayer()
    {
        Vector2 direction = (transform.position - closestClone.position).normalized;
        rb.velocity = direction * FinalSpeed;
    }

    private void RotatePhlegmBullet()
    {
        Vector2 direction = (closestClone.position - transform.position).normalized;
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
