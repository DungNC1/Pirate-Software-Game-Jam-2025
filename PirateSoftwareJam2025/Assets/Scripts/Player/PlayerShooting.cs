using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStats;

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
    private float ShootTimer;
    private GameObject closestEnemy;
    Dictionary<BulletType, int> Ammunitions = new Dictionary<BulletType, int>();
    [SerializeField] int CurrentAmmo = 0;
    [SerializeField] private bool canSpawnMinion = true;
    [SerializeField] private float SpawnTimer;
    [SerializeField] MinionBehaviour Minion;

    private void Awake()
    {
        mainCamera = Camera.main;
        InitAmmunition();
    }

    private void Update()
    {
        FindClosestEnemyInView();
        HandleCreateMinion();

        if (closestEnemy == null)
            return;

        HandleRotation();
        HandleFire();
    }

    private void HandleRotation()
    {
        Vector3 direction = closestEnemy.transform.position - transform.position;
        float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }
    private void HandleFire()
    {
        if (!canFire)
        {
            ShootTimer += Time.deltaTime;

            if (ShootTimer > playerStats.shootCooldown)
            {
                canFire = true;
                ShootTimer = 0;
            }
            return;
        }

        if (!CheckAndUseAmmo())
            return;

        canFire = false;
        switch(playerStats.bulletType) 
        {
            case BulletType.Regular:
                GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().SetDirection(closestEnemy.transform.position);
                break;
            case BulletType.Bounce:
                GameObject bouncingBullet = Instantiate(bouncingBulletPrefab, firePoint.transform.position, Quaternion.identity);
                bouncingBullet.GetComponent<BouncingBullet>().SetDirection(closestEnemy.transform.position);
                break;
            case PlayerStats.BulletType.Stun:
                GameObject stunBullet = Instantiate(stunBulletPrefab, firePoint.transform.position, Quaternion.identity);
                stunBullet.GetComponent<StunBullet>().SetDirection(closestEnemy.transform.position);
                break;
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

    private bool CheckAndUseAmmo()
    {
        if (Ammunitions[playerStats.bulletType] > 0)
        {
            Ammunitions[playerStats.bulletType]--;
            CurrentAmmo = Ammunitions[playerStats.bulletType];
            return true;
        }

        return false;
    }

    void InitAmmunition()
    {
        Ammunitions.Add(BulletType.Regular, 10);
        Ammunitions.Add(BulletType.Bounce, 0);
        Ammunitions.Add(BulletType.Poison, 0);
        Ammunitions.Add(BulletType.Explode, 0);
        Ammunitions.Add(BulletType.Melee, 0);
        Ammunitions.Add(BulletType.Stun, 0);

        CurrentAmmo = Ammunitions[playerStats.bulletType];
    }

    private void HandleCreateMinion()
    {
        if (!canSpawnMinion)
        {
            SpawnTimer += Time.deltaTime;

            if (SpawnTimer > playerStats.MinionCreationCooldown)
            {
                canSpawnMinion = true;
                SpawnTimer = 0;
            }
            return;
        }

        if (!PlayerInputHandler.Instance.GetMinionInput)
            return;

        if (!CheckAndUseAmmo())
            return;

        canSpawnMinion = false;
        MinionBehaviour SpawnedMinion = Instantiate(Minion, transform.position, Quaternion.identity);
        SpawnedMinion.InitMinion(playerStats.bulletType, PlayerInputHandler.Instance.transform);
    }
}
