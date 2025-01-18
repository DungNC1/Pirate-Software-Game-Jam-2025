using UnityEngine;

public class MinionData : ScriptableObject
{
    public float health = 1;
}

[CreateAssetMenu(fileName = "RegularMinionData", menuName = "ScriptableObjects/MinionData/RegularMinionData")]
public sealed class RegularMinionData : MinionData
{

}
[CreateAssetMenu(fileName = "PoisonMinionData", menuName = "ScriptableObjects/MinionData/PoisonMinionData")]
public sealed class PoisonMinionData : MinionData
{

}
[CreateAssetMenu(fileName = "BounceMinionData", menuName = "ScriptableObjects/MinionData/BounceMinionData")]
public sealed class BounceMinionData : MinionData
{

}
[CreateAssetMenu(fileName = "ExplodeMinionData", menuName = "ScriptableObjects/MinionData/ExplodeMinionData")]
public sealed class ExplodeMinionData : MinionData
{

}
[CreateAssetMenu(fileName = "MeleeMinionData", menuName = "ScriptableObjects/MinionData/MeleeMinionData")]
public sealed class MeleeMinionData : MinionData
{

}
[CreateAssetMenu(fileName = "StunMinionData", menuName = "ScriptableObjects/MinionData/StunMinionData")]

public sealed class StunMinionData : MinionData
{

}