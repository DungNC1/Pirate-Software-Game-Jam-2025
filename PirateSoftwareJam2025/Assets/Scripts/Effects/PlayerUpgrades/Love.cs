public class Love : Upgrade
{
    public int HealthBuff;
    public override void ApplyUpgrade()
    {
        PlayerHealth playerHealth = PlayerInputHandler.Instance.GetComponent<PlayerHealth>();
        playerHealth.BuffHealth(HealthBuff);
    }
}
