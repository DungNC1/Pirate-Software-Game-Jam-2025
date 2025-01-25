using UnityEngine;

[CreateAssetMenu(fileName = "Osteoporosis", menuName = "ScriptableObjects/UpgradeData/Osteoporosis")]
public class Osteoporosis : Upgrade
{
    public float BuffPercentage;
    public override void ApplyUpgrade()
    {
        PlayerInputHandler.Instance.GetComponent<PlayerHealth>().OnDamaged.AddListener(GlobalPassiveEffects.Instance.UpdateDamageBuff);
    }
}
