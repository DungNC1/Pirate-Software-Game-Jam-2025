using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : MonoBehaviour
{
    public enum EnemyType { ChasingEnemy, Wheezlin, Tomache }
    public EnemyType enemyType;
    public float slowDownFactor = 0.5f;
    public float slowDownDuration = 3f;
    [HideInInspector] public bool isSlowed;

    public IEnumerator SlowDown()
    {
        switch (enemyType)
        {
            case EnemyType.ChasingEnemy:
                ChasingEnemy chasingEnemy = GetComponent<ChasingEnemy>();
                if (chasingEnemy != null && !isSlowed)
                {
                    isSlowed = true;
                    float originalSpeed = chasingEnemy.speed;
                    chasingEnemy.speed *= slowDownFactor;
                    yield return new WaitForSeconds(slowDownDuration);
                    isSlowed = false;
                    chasingEnemy.speed = originalSpeed;
                }
                break;

            case EnemyType.Wheezlin:
                Wheezlin wheezlin = GetComponent<Wheezlin>();
                if (wheezlin != null && !isSlowed)
                {
                    isSlowed = true;
                    float originalSpeed = wheezlin.speed;
                    wheezlin.speed *= slowDownFactor;
                    yield return new WaitForSeconds(slowDownDuration);
                    isSlowed = false;
                    wheezlin.speed = originalSpeed;
                }
                break;
            case EnemyType.Tomache:
                Tomache tomache = GetComponent<Tomache>();
                if (tomache != null && !isSlowed)
                {
                    isSlowed = true;
                    float originalSpeed = tomache.currentSpeed;
                    tomache.currentSpeed *= slowDownFactor;
                    yield return new WaitForSeconds(slowDownDuration);
                    isSlowed = false;
                    tomache.currentSpeed = originalSpeed;
                }
                break;
        }
    }
}
