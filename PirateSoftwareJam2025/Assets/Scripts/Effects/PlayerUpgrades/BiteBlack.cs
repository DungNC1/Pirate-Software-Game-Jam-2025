using UnityEngine;

[CreateAssetMenu(fileName = "BiteBack", menuName = "ScriptableObjects/UpgradeData/BiteBack")]
public class BiteBack : Upgrade
{
    public float Range = 1f;
    public int Damage = 10;
    public LayerMask LayerMask;
    public GameObject biteEffectPrefab;

    public override void ApplyUpgrade()
    {
        PlayerHealth playerHealth = PlayerInputHandler.Instance.GetComponent<PlayerHealth>();
        playerHealth.OnDamagedPosition.AddListener(BiteBackAOE);
    }

    public void BiteBackAOE(Vector3 position)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, Range, LayerMask);
        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                damagable.TakeDamage(Damage);
            }
        }

        if (biteEffectPrefab != null)
        {
            Instantiate(biteEffectPrefab, position, Quaternion.identity);
        }
    }
}
