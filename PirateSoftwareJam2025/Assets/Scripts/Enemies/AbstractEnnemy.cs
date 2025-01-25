using UnityEngine;

public abstract class AbstractEnnemy : MonoBehaviour
{
    public float speed = 5f;
    protected float FinalSpeed;
    public float GetSpeed { get { return FinalSpeed; } }
    public int xpValue = 1;
    [SerializeField] Experience ExperiencePiece;
    public virtual void Start()
    {
        FinalSpeed = speed;
        GlobalPassiveEffects.Instance.SlowDebuffChange.AddListener(ChangeSpeed);
    }

    public virtual void Die()
    {
        Instantiate(ExperiencePiece,transform.position,Quaternion.identity);
        if (!GlobalPassiveEffects.Instance.RollGutsChance())
            return;
        Instantiate(ExperiencePiece, transform.position, Quaternion.identity);
    }

    public void ChangeSpeed(float percentage)
    {
        FinalSpeed = speed * (1 - percentage);
    }
}
