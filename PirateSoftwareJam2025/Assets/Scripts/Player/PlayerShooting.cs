using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera mainCamera;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bouncingBulletPrefab;
    [SerializeField] private GameObject stunBulletPrefab;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform firePoint;
    private bool canFire;
    private float timer;
    private GameObject closestEnemy;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        FindClosestEnemyInView();

        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.transform.position - transform.position;
            float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, zRotation);

            if (!canFire)
            {
                timer += Time.deltaTime;

                if (timer > playerStats.shootCooldown)
                {
                    canFire = true;
                    timer = 0;
                }
            }

            if (canFire)
            {
                canFire = false;
                switch(playerStats.bulletType) 
                {
                    case PlayerStats.BulletType.Regular:
                        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
                        bullet.GetComponent<Bullet>().SetDirection(closestEnemy.transform.position);
                        break;
                    case PlayerStats.BulletType.Bounce:
                        GameObject bouncingBullet = Instantiate(bouncingBulletPrefab, firePoint.transform.position, Quaternion.identity);
                        bouncingBullet.GetComponent<BouncingBullet>().SetDirection(closestEnemy.transform.position);
                        break;
                    case PlayerStats.BulletType.Stun:
                        GameObject stunBullet = Instantiate(stunBulletPrefab, firePoint.transform.position, Quaternion.identity);
                        stunBullet.GetComponent<StunBullet>().SetDirection(closestEnemy.transform.position);
                        break;
                }
            }
        }
    }

    private void FindClosestEnemyInView()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(enemy.transform.position);
            if (viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    closestEnemy = enemy;
                    minDistance = distance;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (mainCamera != null && closestEnemy != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, closestEnemy.transform.position);
        }
    }
}
