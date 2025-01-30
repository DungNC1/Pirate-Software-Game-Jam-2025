using UnityEngine;
using static PlayerStats;

public abstract class AbstractBullet : MonoBehaviour
{
    private bool isActive = true;
    public bool GetIsActive { get { return isActive; } }
    protected Rigidbody2D rb;
    public BulletType bulletType;
    public float damage = 1f;
    protected float FinalDamage = 1;
    private GameObject player;
    protected Vector3 Direction = Vector3.zero;
    public bool MinionBullet = false;

    public virtual void Awake()
    {
        TryGetComponent(out rb);
        ChangeDamage(GlobalPassiveEffects.Instance.PlayerDamageBuffPercentageIncrement);
    }

    protected virtual void Start()
    {
        FinalDamage = damage;
        Debug.Log(FinalDamage);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void InitParameters(Vector3 Target, bool shotByMinion)
    {
        Direction = Target - transform.position;
        MinionBullet = shotByMinion;
    }

    protected void SetInactive()
    {
        if (rb)
            rb.velocity = Vector3.zero;

        isActive = false;
        InvokeRepeating("UpdateRotation", 0, 0.1f); 
    }

    private void UpdateRotation()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    public void ChangeDamage(float percentage)
    {
        FinalDamage = damage * (1 + percentage);
    }

    protected void ComputeFinalDamageDealt()
    {
        if (!MinionBullet)
            return;

        FinalDamage /= 2;
    }
}
