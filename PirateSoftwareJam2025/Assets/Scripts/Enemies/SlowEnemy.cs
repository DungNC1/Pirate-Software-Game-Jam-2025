using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : MonoBehaviour
{
    public enum EnemyType { ChasingEnemy, Type2 }
    public EnemyType enemyType;
    public float slowDownFactor = 0.5f;
    public float slowDownDuration = 2f;

    public IEnumerator SlowDown()
    {
        switch (enemyType)
        {
            case EnemyType.ChasingEnemy:
                ChasingEnemy chasingEnemy = GetComponent<ChasingEnemy>();
                if (chasingEnemy != null)
                {
                    float originalSpeed = chasingEnemy.speed;
                    chasingEnemy.speed *= slowDownFactor;
                    yield return new WaitForSeconds(slowDownDuration);
                    chasingEnemy.speed = originalSpeed;
                }
                break;

            /*case EnemyType.Type2:
                EnemyMovementType2 movementType2 = GetComponent<EnemyMovementType2>();
                if (movementType2 != null)
                {
                    float originalSpeed = movementType2.speed;
                    movementType2.speed *= slowDownFactor;
                    yield return new WaitForSeconds(slowDownDuration);
                    movementType2.speed = originalSpeed;
                }
                break;*/
        }
    }
}
