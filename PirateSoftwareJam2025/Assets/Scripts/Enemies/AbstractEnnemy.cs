using System;
using UnityEngine;
using static PlayerStats;

public abstract class AbstractEnnemy : MonoBehaviour
{
    public float speed = 5f;
    protected float FinalSpeed;
    public float GetSpeed { get { return FinalSpeed; } }
    public int xpValue = 1;
    [SerializeField] Experience ExperiencePiece;
    private Rigidbody2D RB;
    private SpriteRenderer SR;
    private Collider2D collider2D;

    public virtual void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        SR = GetComponentInChildren<SpriteRenderer>();

        if (RB == null)
        {
            Debug.LogError("Rigidbody2D is missing on " + gameObject.name);
        }
        if (SR == null)
        {
            Debug.LogError("SpriteRenderer is missing on " + gameObject.name);
        }

        FinalSpeed = speed;
        GlobalPassiveEffects.Instance.SlowDebuffChange.AddListener(ChangeSpeed);
        ChangeSpeed(GlobalPassiveEffects.Instance.EnnemiesSlowDebuffPercentage);
    }

    protected virtual void Update()
    {
        if (RB != null)
        {
            if (RB.velocity.x < 0)
                SR.flipX = true;
            else
                SR.flipX = false;
        }
    }

    public virtual void Die()
    {
        Experience SpawnedExperiencePiece = Instantiate(ExperiencePiece, transform.position, Quaternion.identity);
        SpawnedExperiencePiece.Init(xpValue);
        DropRandomBullet();
        Destroy(gameObject);
        if (!GlobalPassiveEffects.Instance.RollGutsChance())
            return;
        SpawnedExperiencePiece = Instantiate(ExperiencePiece, transform.position, Quaternion.identity);
        SpawnedExperiencePiece.Init(xpValue);
    }

    public void ChangeSpeed(float percentage)
    {
        FinalSpeed = speed * (1 - percentage);
    }

    void DropRandomBullet()
    {
        collider2D.enabled = false;
        Array values = Enum.GetValues(typeof(BulletType));
        System.Random random = new System.Random();
        BulletType randomAmmo = (BulletType)values.GetValue(random.Next(values.Length));
        AbstractBullet spawnedBullet = BulletGiver.Instance.GetBullet(randomAmmo);
        spawnedBullet.InitParameters(spawnedBullet.transform.position, false);
        spawnedBullet.transform.position = transform.position;
    }
}
