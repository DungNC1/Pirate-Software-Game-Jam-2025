using UnityEngine;

public class ChasingEnemy : AbstractEnnemy
{
    private Transform player;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x, direction.y) * FinalSpeed;
        }
    }
}
