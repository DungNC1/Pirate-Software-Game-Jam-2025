using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollector : MonoBehaviour
{
    [SerializeField] private float m_CollectRadius = 1;
    [SerializeField, Range(0f,1f)] private float m_AttractionForce = 1f;
    bool m_InfiniteRadius = false;
    [SerializeField] List<AbstractBullet> AttractedBullets = new List<AbstractBullet>();
    CircleCollider2D m_CircleCollider;
    [SerializeField] PlayerShooting m_PlayerShooting;
    private void Awake()
    {
        TryGetComponent(out m_CircleCollider);
    }

    public void SetInfiniteRadius()
    {
        m_InfiniteRadius = true;
    }

    private void Update()
    {
        UpdateColliderParameters();
        HandleAmmoCollect();
    }

    void HandleAmmoCollect()
    {
        foreach (AbstractBullet bullet in AttractedBullets)
        {
            if (!bullet.GetIsActive)
                bullet.transform.position = Vector3.Lerp(bullet.transform.position, transform.position, m_AttractionForce * Time.deltaTime);
        }
    }

    void UpdateColliderParameters()
    {
        if (!m_InfiniteRadius)
            m_CircleCollider.radius = m_CollectRadius;
        else
            m_CircleCollider.radius = 999;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AbstractBullet bullet;
        if (collision.TryGetComponent(out bullet))
        {
            AttractedBullets.Add(bullet);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        AbstractBullet bullet;
        if (collision.TryGetComponent(out bullet))
        {
            AttractedBullets.Remove(bullet);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AbstractBullet bullet;
        if (collision.transform.TryGetComponent(out bullet))
        {
            AttractedBullets.Remove(bullet);
            m_PlayerShooting.AddAmmo(bullet.bulletType, 1);
            Destroy(bullet.gameObject);
        }
    }
}
