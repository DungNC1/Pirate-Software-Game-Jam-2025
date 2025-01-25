using UnityEngine;

[CreateAssetMenu(fileName = "Guts", menuName = "ScriptableObjects/UpgradeData/Guts")]
public class Guts : Upgrade
{
    [Range(0, 1f)] public float Percentage;
    public override void ApplyUpgrade()
    {
        GlobalPassiveEffects.Instance.ActivateGuts(Percentage);
    }
}
