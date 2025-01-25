
using UnityEngine;

[CreateAssetMenu(fileName = "ChipGalore", menuName = "ScriptableObjects/UpgradeData/ChipGalore")]
public class ChipGalore : Upgrade
{
    [Range(0,1f)] public float Percentage;
    public override void ApplyUpgrade()
    {
        GlobalPassiveEffects.Instance.ActivateGalore(Percentage);
    }
}