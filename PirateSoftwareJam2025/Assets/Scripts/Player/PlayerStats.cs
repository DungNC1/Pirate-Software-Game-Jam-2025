using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public float Speed = 5;
    public float shootCooldown = 0.5f;
    public float MinionCreationCooldown = 0.2f;
    public enum BulletType {
        Regular,
        Poison,
        Bounce,
        Explode,
        Melee,
        Stun
    }
    public BulletType bulletType;
}
