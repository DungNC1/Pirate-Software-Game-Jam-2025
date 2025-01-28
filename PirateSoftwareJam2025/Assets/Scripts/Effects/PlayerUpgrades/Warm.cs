using UnityEngine;

[CreateAssetMenu(fileName = "Warm", menuName = "ScriptableObjects/UpgradeData/Warm")]
public class Warm : Upgrade
{
    public float DebuffAmount;
    public override void ApplyUpgrade()
    {
        GlobalPassiveEffects.Instance.UpdatePVDebuff(DebuffAmount);
        GlobalPassiveEffects.Instance.UpdateSlowDebuff(DebuffAmount);
    }
}
