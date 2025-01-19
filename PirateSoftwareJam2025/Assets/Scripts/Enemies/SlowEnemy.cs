using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : MonoBehaviour
{
    public enum EnemyType { ChasingEnemy, Wheezlin }
    public EnemyType enemyType;
    public float slowDownFactor = 0.5f;
    public float slowDownDuration = 3f;

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

            case EnemyType.Wheezlin:
                Wheezlin wheezlin = GetComponent<Wheezlin>();
                if (wheezlin != null)
                {
                    float originalSpeed = wheezlin.speed;
                    wheezlin.speed *= slowDownFactor;
                    yield return new WaitForSeconds(slowDownDuration);
                    wheezlin.speed = originalSpeed;
                }
                break;
        }
    }
}
