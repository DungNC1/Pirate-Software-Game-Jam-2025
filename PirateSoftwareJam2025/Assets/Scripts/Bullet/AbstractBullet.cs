using UnityEngine;
using static PlayerStats;

public abstract class AbstractBullet : MonoBehaviour
{
    private bool isActive = true;
    public bool GetIsActive {  get { return isActive; } }
    protected Rigidbody2D rb;
    public BulletType bulletType;

    public virtual void Awake()
    {
        TryGetComponent(out rb);
    }

    protected void SetInactive()
    {
        if(rb)
            rb.velocity = Vector3.zero;

        isActive = false;
        transform.rotation = Quaternion.identity;
    }
}
