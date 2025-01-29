using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static PlayerStats;

public class PlayerShooting : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera mainCamera;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private BouncingBullet bouncingBulletPrefab;
    [SerializeField] private StunBullet stunBulletPrefab;
    [SerializeField] private PoisonBullet poisonBulletPrefab;
    [SerializeField] private ExplodingBullet explodeBulletPrefab;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform firePoint;
    [HideInInspector] public float shootCooldown;
    private bool canFire;
    private float ShootTimer;
    private GameObject closestEnemy;
    Dictionary<BulletType, int> Ammunitions = new Dictionary<BulletType, int>();
    List<BulletType> bulletTypesCycleTracker = new List<BulletType>();
    [SerializeField] int CurrentAmmo = 0;
    int currentAmmoIndex = 0;
    [SerializeField] private bool canSpawnMinion = true;
    [SerializeField] private float SpawnTimer;
    [SerializeField] MinionBehaviour Minion;
    List<MinionBehaviour> MinionBehaviours = new List<MinionBehaviour>();
    [SerializeField] private int MaxMinion = 2;
    private bool LockMinionNumber = false;
    private float timer;
    [SerializeField] int MinionCost = 2;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        PlayerInputHandler.Instance.GetScrollDownEvent.AddListener(ChangeAmmo);
        PlayerInputHandler.Instance.GetScrollUpEvent.AddListener(ChangeAmmo);
        InitAmmunition();
        shootCooldown = playerStats.shootCooldown;
    }

    private void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;
        float zRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
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

        if (Input.GetMouseButton(0))
        {
            HandleFire();
        }

        HandleCreateMinion();

        if (closestEnemy == null)
            return;

        HandleRotation();
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
            return;
        
        if (!CheckAndUseAmmo(1))
            return;

        canFire = false;
        AbstractBullet spawnedBullet = null;
        switch(playerStats.bulletType) 
        {
            case BulletType.Regular:
                spawnedBullet = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
                break;
            case BulletType.Bounce:
                spawnedBullet = Instantiate(bouncingBulletPrefab, firePoint.transform.position, Quaternion.identity);
                break;
            case BulletType.Stun:
                spawnedBullet = Instantiate(stunBulletPrefab, firePoint.transform.position, Quaternion.identity);
                break;
            case BulletType.Poison:
                spawnedBullet = Instantiate(poisonBulletPrefab, firePoint.transform.position, Quaternion.identity);
                break;
            case BulletType.Explode:
                spawnedBullet = Instantiate(explodeBulletPrefab, firePoint.transform.position, Quaternion.identity);
                break;
        }
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        spawnedBullet.InitParameters(mousePosition, false);
    }

    private bool CheckAndUseAmmo(int amount)
    {
        if (Ammunitions[playerStats.bulletType] >= amount)
        {
            Ammunitions[playerStats.bulletType] -= amount;
            CurrentAmmo = Ammunitions[playerStats.bulletType];
            return true;
        }

        return false;
    }

    public void AddAmmo(BulletType type, int ammount)
    {
        Ammunitions[type] += ammount;
        if (GlobalPassiveEffects.Instance.RollGaloreChance())
            Ammunitions[type] += ammount;
        
    }

    void InitAmmunition()
    {
        Ammunitions.Add(BulletType.Regular, 10);
        Ammunitions.Add(BulletType.Bounce, 10);
        Ammunitions.Add(BulletType.Poison, 10);
        Ammunitions.Add(BulletType.Explode, 10);
        Ammunitions.Add(BulletType.Melee, 10);
        Ammunitions.Add(BulletType.Stun, 10);

        bulletTypesCycleTracker.Add(BulletType.Regular);
        bulletTypesCycleTracker.Add(BulletType.Bounce);
        bulletTypesCycleTracker.Add(BulletType.Poison);
        bulletTypesCycleTracker.Add(BulletType.Explode);
        bulletTypesCycleTracker.Add(BulletType.Melee);
        bulletTypesCycleTracker.Add(BulletType.Stun);


        CurrentAmmo = Ammunitions[bulletTypesCycleTracker[currentAmmoIndex]];
        AmmoSelectorUI.Instance.SetSelector(currentAmmoIndex);
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

        if (MinionBehaviours.Count >= MaxMinion)
            return;

        if (!CheckAndUseAmmo(MinionCost))
            return;

        canSpawnMinion = false;
        MinionBehaviour SpawnedMinion = Instantiate(Minion, transform.position, Quaternion.identity);
        SpawnedMinion.InitMinion(playerStats.bulletType, PlayerInputHandler.Instance.transform);
        MinionBehaviours.Add(SpawnedMinion);
        SpawnedMinion.MinionDie.AddListener(RemoveMinion);
    }

    void ChangeAmmo(int change)
    {
        currentAmmoIndex += change;

        if(currentAmmoIndex < 0)
            currentAmmoIndex = bulletTypesCycleTracker.Count - 1;
        if (currentAmmoIndex == bulletTypesCycleTracker.Count)
            currentAmmoIndex = 0;

        playerStats.bulletType = bulletTypesCycleTracker[currentAmmoIndex];
        CurrentAmmo = Ammunitions[bulletTypesCycleTracker[currentAmmoIndex]];

        AmmoSelectorUI.Instance.SetSelector(currentAmmoIndex);
    }

    void RemoveMinion(MinionBehaviour Minion)
    {
        MinionBehaviours.Remove(Minion);
    }

    public void AddMinionLimit(int amount)
    {
        MaxMinion += amount;
    }

    public void SetLockMinionNumber()
    {
        LockMinionNumber = true;
        MaxMinion = 1;
        for(int i = 0; i < MinionBehaviours.Count -1;i++)
        {
            Destroy(MinionBehaviours[0].gameObject);
        }
    }

    public void ConvertAllAmmoToOne()
    {
        int allAmmo = 0;
        List<BulletType> keys = new List<BulletType>(Ammunitions.Keys); 

        foreach (BulletType key in keys)
        {
            allAmmo += Ammunitions[key];
            Ammunitions[key] = 0;
        }

        Array values = Enum.GetValues(typeof(BulletType));
        System.Random random = new System.Random();
        BulletType randomAmmo = (BulletType)values.GetValue(random.Next(values.Length));
        Ammunitions[randomAmmo] = allAmmo;

        currentAmmoIndex = bulletTypesCycleTracker.IndexOf(randomAmmo);
        playerStats.bulletType = randomAmmo;
        CurrentAmmo = Ammunitions[bulletTypesCycleTracker[currentAmmoIndex]];

        AmmoSelectorUI.Instance.SetSelector(currentAmmoIndex);
    }

}
