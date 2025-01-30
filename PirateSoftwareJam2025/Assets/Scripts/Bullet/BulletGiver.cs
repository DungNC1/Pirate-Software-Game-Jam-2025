using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStats;

public class BulletGiver : MonoBehaviour
{
    public static BulletGiver Instance;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private BouncingBullet bouncingBulletPrefab;
    [SerializeField] private StunBullet stunBulletPrefab;
    [SerializeField] private PoisonBullet poisonBulletPrefab;
    [SerializeField] private ExplodingBullet explodeBulletPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);

        Instance = this;
    }
    public AbstractBullet GetBullet(BulletType bulletType)
    {
        AbstractBullet spawnedBullet = null;
        switch (bulletType)
        {
            case BulletType.Regular:
                spawnedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                break;
            case BulletType.Bounce:
                spawnedBullet = Instantiate(bouncingBulletPrefab, transform.position, Quaternion.identity);
                break;
            case BulletType.Stun:
                spawnedBullet = Instantiate(stunBulletPrefab, transform.position, Quaternion.identity);
                break;
            case BulletType.Poison:
                spawnedBullet = Instantiate(poisonBulletPrefab, transform.position, Quaternion.identity);
                break;
            case BulletType.Explode:
                spawnedBullet = Instantiate(explodeBulletPrefab, transform.position, Quaternion.identity);
                break;
        }
        return spawnedBullet;
    }
}
