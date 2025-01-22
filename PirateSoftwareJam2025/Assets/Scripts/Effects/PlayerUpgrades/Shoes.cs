public class Shoes : Upgrade
{
    public float SpeedBuff = 1;
    public override void ApplyUpgrade()
    {
        PlayerMovement movement = PlayerInputHandler.Instance.GetComponent<PlayerMovement>();
        movement.speed += SpeedBuff;
    }
}
