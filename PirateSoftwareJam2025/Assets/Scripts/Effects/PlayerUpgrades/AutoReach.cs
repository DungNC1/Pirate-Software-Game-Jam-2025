using UnityEngine;

[CreateAssetMenu(fileName = "AutoReach", menuName = "ScriptableObjects/UpgradeData/AutoReach")]
public class AutoReach : Upgrade
{
    public override void ApplyUpgrade()
    {
        AmmoCollector collector = PlayerInputHandler.Instance.GetComponentInChildren<AmmoCollector>();
        collector.SetInfiniteRadius();
    }
}
