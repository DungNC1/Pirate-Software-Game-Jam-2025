using UnityEngine;

public abstract class AbstractEnnemy : MonoBehaviour
{
    public float speed = 5f;
    protected float FinalSpeed;
    public float GetSpeed { get { return FinalSpeed; } }
    public int xpValue = 1;
    [SerializeField] Experience ExperiencePiece;
    private Rigidbody2D RB;
    private SpriteRenderer SR;

    public virtual void Start()
    {
        Debug.Log("Start");
        FinalSpeed = speed;

        GlobalPassiveEffects.Instance.SlowDebuffChange.AddListener(ChangeSpeed);

        ChangeSpeed(GlobalPassiveEffects.Instance.EnnemiesSlowDebuffPercentage);

        RB = GetComponent<Rigidbody2D>();
        SR = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (RB.velocity.x < 0)
            SR.flipX = true;
        else
            SR.flipX = false;
    }

    public virtual void Die()
    {
        Experience SpawnedExperiencePiece = Instantiate(ExperiencePiece, transform.position, Quaternion.identity);
        SpawnedExperiencePiece.Init(xpValue);
        if (!GlobalPassiveEffects.Instance.RollGutsChance())
            return;
        SpawnedExperiencePiece = Instantiate(ExperiencePiece, transform.position, Quaternion.identity);
        SpawnedExperiencePiece.Init(xpValue);
    }

    public void ChangeSpeed(float percentage)
    {
        FinalSpeed = speed * (1 - percentage);
    }
}
