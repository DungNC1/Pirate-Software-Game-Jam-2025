using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static PlayerStats;

public class MinionBehaviour : MonoBehaviour, IDamagable
{
    BulletType m_BulletType = BulletType.Regular;
    Transform m_Player;
    Rigidbody2D m_Rigidbody2D;

    [Header("Paramaters")]
    [SerializeField] private float m_MinDistanceToPlayer = 1f;

    [Header("Stats")]
    [SerializeField] private float m_Speed = 5f;
    [SerializeField] private float m_Health = 1f;
    [SerializeField] private float m_AttackCD = 1f;
    [SerializeField] private float m_AttackCDDecreasing = 0f;

    [Header("Minion Datas")]
    [SerializeField] MinionData m_RegularMinionData;
    [SerializeField] MinionData m_PoisonMinionData;
    [SerializeField] MinionData m_BounceMinionData;
    [SerializeField] MinionData m_ExplodeMinionData;
    [SerializeField] MinionData m_MeleeMinionData;
    [SerializeField] MinionData m_StunMinionData;
    List<AbstractEnnemy> EnnemiesInRange = new List<AbstractEnnemy>();
    AbstractEnnemy closestEnnemy = null;
    public UnityEvent<MinionBehaviour> MinionDie = new UnityEvent<MinionBehaviour>();

    [SerializeField] CircleCollider2D circleCollider2D;

    private void Awake()
    {
        TryGetComponent(out m_Rigidbody2D);
    }
    public void InitMinion(BulletType bulletType, Transform Player)
    {
        m_BulletType = bulletType;
        m_Player = Player;

        switch (bulletType)
        {
            case BulletType.Regular:
                LoadData(m_RegularMinionData);
                break;
            case BulletType.Poison:
                LoadData(m_PoisonMinionData);
                break;
            case BulletType.Bounce:
                LoadData(m_BounceMinionData);
                break;
            case BulletType.Explode:
                LoadData(m_ExplodeMinionData);
                break;
            case BulletType.Melee:
                LoadData(m_MeleeMinionData);
                break;
            case BulletType.Stun:
                LoadData(m_StunMinionData);
                break;
            default: break;
        }
    }

    private void FixedUpdate()  
    {
        HandleMovement();
    }

    private void Update()
    {
        HandleAggro();
        HandleAttack();
    }

    private void HandleMovement()
    {
        Vector3 PlayerVector = m_Player.position - transform.position;
        if (PlayerVector.magnitude > m_MinDistanceToPlayer)
            m_Rigidbody2D.velocity = PlayerVector.normalized * m_Speed;
        else
            m_Rigidbody2D.velocity = Vector2.zero;
    }

    private void LoadData(MinionData minionData)
    {
        m_Health = minionData.health;
        circleCollider2D.radius = minionData.AggroRadius;
        m_AttackCD = minionData.AttackCooldown;
    }
    public void TakeDamage(int damage)
    {

    }

    private void OnDestroy()
    {
        MinionDie.Invoke(this);
    }

    void HandleAggro()
    {
        if (EnnemiesInRange.Count == 0)
            return;

        closestEnnemy = GetClosestEnnemy();
    }

    AbstractEnnemy GetClosestEnnemy()
    {
        AbstractEnnemy returnEnnemy =  null;

        float minDistance = Mathf.Infinity;

        foreach (AbstractEnnemy enemy in EnnemiesInRange)
        {
            Vector3 viewportPoint = Camera.main.WorldToViewportPoint(enemy.transform.position);
            if (viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    returnEnnemy = enemy;
                    minDistance = distance;
                }
            }
        }
        return returnEnnemy;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        AbstractEnnemy ennemy = null;
        collider.TryGetComponent(out ennemy);
        if (ennemy == null)
            return;

        EnnemiesInRange.Add(ennemy);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        AbstractEnnemy ennemy = null;
        collider.TryGetComponent(out ennemy);
        if (ennemy == null)
            return;

        EnnemiesInRange.Remove(ennemy);
    }

    void HandleAttack()
    {
        if(m_AttackCD > m_AttackCDDecreasing)
        {
            m_AttackCDDecreasing += Time.deltaTime;
            return;
        }

        if (closestEnnemy == null)
            return;

        m_AttackCDDecreasing = 0;

        AbstractBullet spawnedBullet = BulletGiver.Instance.GetBullet(m_BulletType);
        spawnedBullet.transform.position = transform.position;
        Vector3 target = closestEnnemy.transform.position;
        spawnedBullet.InitParameters(target, true);
    }
}
