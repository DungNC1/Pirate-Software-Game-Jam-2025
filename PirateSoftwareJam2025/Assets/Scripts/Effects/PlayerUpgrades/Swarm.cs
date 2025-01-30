using UnityEngine;

[CreateAssetMenu(fileName = "Swarm", menuName = "ScriptableObjects/UpgradeData/Swarm")]
public class Swarm : Upgrade
{
    public int MinionSlotBuff = 2;
    public override void ApplyUpgrade()
    {
        PlayerShooting playerShooting = PlayerInputHandler.Instance.GetComponentInChildren<PlayerShooting>();
        playerShooting.AddMinionLimit(MinionSlotBuff);
    }
}
