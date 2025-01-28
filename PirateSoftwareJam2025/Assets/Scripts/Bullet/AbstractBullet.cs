using UnityEngine;
using static PlayerStats;

public abstract class AbstractBullet : MonoBehaviour
{
    private bool isActive = true;
    public bool GetIsActive {  get { return isActive; } }
    protected Rigidbody2D rb;
    public BulletType bulletType;
    public float damage = 1f;
    protected float FinalDamage = 1;

    public virtual void Awake()
    {
        TryGetComponent(out rb);
        GlobalPassiveEffects.Instance.SlowDebuffChange.AddListener(ChangeDamage);
    }

    protected virtual void Start()
    {
        FinalDamage = damage;
        Debug.Log(FinalDamage);
    }

    protected void SetInactive()
    {
        if(rb)
            rb.velocity = Vector3.zero;

        isActive = false;
        transform.rotation = Quaternion.identity;
    }

    public void ChangeDamage(float percentage)
    {
        FinalDamage = damage * (1 + percentage);
    }
}
