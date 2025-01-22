public class LoneWolf : Upgrade
{
    public override void ApplyUpgrade()
    {
        PlayerShooting playerShooting = PlayerInputHandler.Instance.GetComponent<PlayerShooting>();
        playerShooting.SetLockMinionNumber();
    }
}
