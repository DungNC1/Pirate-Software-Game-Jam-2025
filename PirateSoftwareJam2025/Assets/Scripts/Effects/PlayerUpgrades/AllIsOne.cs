using UnityEngine;

[CreateAssetMenu(fileName = "AllIsOne", menuName = "ScriptableObjects/UpgradeData/AllIsOne")]
public class AllIsOne : Upgrade
{
    public override void ApplyUpgrade()
    {
        PlayerInputHandler.Instance.GetComponentInChildren<PlayerShooting>().ConvertAllAmmoToOne();
    }
}
