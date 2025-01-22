using UnityEngine;

public abstract class AbstractEnnemy : MonoBehaviour
{
    public float speed = 5f;
    public int xpValue = 1;

    public virtual void Die()
    {
        PlayerXP.instance.GainXP(xpValue);
    }
}
