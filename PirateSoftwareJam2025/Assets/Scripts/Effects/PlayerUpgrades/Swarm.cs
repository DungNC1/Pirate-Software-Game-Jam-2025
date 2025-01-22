
public class Swarm : Upgrade
{
    public int MinionSlotBuff = 2;
    public override void ApplyUpgrade()
    {
        PlayerShooting playerShooting = PlayerInputHandler.Instance.GetComponent<PlayerShooting>();
        playerShooting.AddMinionLimit(MinionSlotBuff);
    }
}
