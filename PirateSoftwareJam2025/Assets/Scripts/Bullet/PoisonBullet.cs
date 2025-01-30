using UnityEngine;
using System.Collections;

public class PoisonBullet : AbstractBullet
{
    [SerializeField] private float poisonDamage = 1f;
    [SerializeField] private float poisonDuration = 3f;
    [SerializeField] private float poisonInterval = 1f;
    [SerializeField] private float force = 10f;
    private Vector3 mousePosition;
    private Camera mainCamera;

    public override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamagable enemyHealth = collision.gameObject.GetComponent<IDamagable>();

            if (enemyHealth != null)
            {
                StartCoroutine(ApplyPoisonDamage(enemyHealth));
            }
        }
    }

    private IEnumerator ApplyPoisonDamage(IDamagable enemyHealth)
    {
        float elapsedTime = 0f;
        while (elapsedTime < poisonDuration)
        {
            enemyHealth.TakeDamage((int)poisonDamage);
            CreateIndicator((int)poisonDamage);
            elapsedTime += poisonInterval;
            yield return new WaitForSeconds(poisonInterval);
        }

        SetInactive();
    }
}
