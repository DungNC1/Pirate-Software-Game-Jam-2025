public class AutoReach : Upgrade
{
    public override void ApplyUpgrade()
    {
        AmmoCollector collector = PlayerInputHandler.Instance.GetComponentInChildren<AmmoCollector>();
        collector.SetInfiniteRadius();
    }
}
