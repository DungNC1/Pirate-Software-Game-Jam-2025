using UnityEngine;

[CreateAssetMenu(fileName = "LoneWolf", menuName = "ScriptableObjects/UpgradeData/LoneWolf")]
public class LoneWolf : Upgrade
{
    public override void ApplyUpgrade()
    {
        PlayerShooting playerShooting = PlayerInputHandler.Instance.GetComponentInChildren<PlayerShooting>();
        PlayerMovement playerMovement = PlayerInputHandler.Instance.GetComponent<PlayerMovement>();
        playerMovement.speed += 5f;
        GlobalPassiveEffects.Instance.UpdateDamageBuff(1);
        playerShooting.SetLockMinionNumber();
    }
}
