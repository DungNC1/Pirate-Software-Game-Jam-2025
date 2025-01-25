using UnityEngine;

public class Deathurus : AbstractEnnemy
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 3f;
    private Transform closestClone;
    private Rigidbody2D rb;
    private bool canAttack = true;
    private float attackTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Start()
    {
        base.Start();
        FindClosestClone();
    }

    private void Update()
    {
        if (closestClone != null)
        {
            MoveTowardsClone();
            if (Vector2.Distance(transform.position, closestClone.position) <= attackRange)
            {
                if (canAttack)
                {
                    AttackClone();
                }
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
        rb.velocity = direction * FinalSpeed;
    }

    private void AttackClone()
    {
        PlayerHealth playerHealth = closestClone.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
        canAttack = false;
    }
}
