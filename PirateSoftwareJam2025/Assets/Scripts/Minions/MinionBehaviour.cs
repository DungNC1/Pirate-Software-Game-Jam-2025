using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [Header("Minion Datas")]
    [SerializeField] MinionData m_RegularMinionData;
    [SerializeField] MinionData m_PoisonMinionData;
    [SerializeField] MinionData m_BounceMinionData;
    [SerializeField] MinionData m_ExplodeMinionData;
    [SerializeField] MinionData m_MeleeMinionData;
    [SerializeField] MinionData m_StunMinionData;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
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
    }
    public void TakeDamage(int damage)
    {
        
    }
}
