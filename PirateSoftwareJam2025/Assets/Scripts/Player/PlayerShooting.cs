using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform firePoint;
    private bool canFire;
    private float timer;
    private GameObject closestEnemy;

    private void Update()
    {
        FindClosestEnemy();

        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.transform.position - transform.position;
            float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, zRotation);

            Debug.DrawLine(transform.position, closestEnemy.transform.position, Color.red);

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
                GameObject firedBullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
                Bullet bulletComponent = firedBullet.GetComponent<Bullet>();
                bulletComponent.SetDirection(closestEnemy.transform.position);
            }
        }
    }

    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
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
