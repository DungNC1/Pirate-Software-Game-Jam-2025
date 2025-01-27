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
        Experience SpawnedExperiencePiece = Instantiate(ExperiencePiece,transform.position,Quaternion.identity);
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
