using UnityEngine;

[CreateAssetMenu(fileName = "Cold", menuName = "ScriptableObjects/UpgradeData/Cold")]
public class Cold : Upgrade
{
    public float DebuffAmount;
    public override void ApplyUpgrade()
    {
        GlobalPassiveEffects.Instance.UpdateSlowDebuff(DebuffAmount);
    }
}
