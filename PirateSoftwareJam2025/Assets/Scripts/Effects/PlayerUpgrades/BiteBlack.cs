using UnityEngine;

[CreateAssetMenu(fileName = "BiteBlack", menuName = "ScriptableObjects/UpgradeData/BiteBlack")]
public class BiteBlack : Upgrade
{
    public float Range = 1;
    public int Damage = 1;
    public LayerMask LayerMask;
    public override void ApplyUpgrade()
    {
        PlayerHealth playerHealth = PlayerInputHandler.Instance.GetComponent<PlayerHealth>();
        playerHealth.OnDamagedPosition.AddListener(BiteBlackAOE);
    }

    public void BiteBlackAOE(Vector3 Position)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(Position, Range, LayerMask);
        foreach (Collider2D hit in hits)
        {
            IDamagable Damagable;
            if (hit.TryGetComponent<IDamagable>(out Damagable))
            {
                Damagable.TakeDamage(Damage);
            }
        }
    }
}
