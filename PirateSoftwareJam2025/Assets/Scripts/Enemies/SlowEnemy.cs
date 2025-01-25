using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : MonoBehaviour
{
    public enum EnemyType { ChasingEnemy, Wheezlin, Tomache, Charvader, Phlennon, Deathrus }
    public EnemyType enemyType;
    public float slowDownFactor = 0.5f;
    public float slowDownDuration = 3f;
    [HideInInspector] public bool isSlowed;

    public IEnumerator SlowDown()
    {
        AbstractEnnemy ennemy = GetComponent<AbstractEnnemy>();
        if (ennemy != null && !isSlowed)
        {
            isSlowed = true;
            GlobalPassiveEffects.Instance.UpdateSlowDebuff(slowDownFactor);
            yield return new WaitForSeconds(slowDownDuration);
            isSlowed = false;
            GlobalPassiveEffects.Instance.UpdateSlowDebuff(-slowDownFactor);
        }
    }
}
